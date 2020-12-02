import {
  Component,
  EventEmitter,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DataServiceService } from '../core/data-service.service';
import { Album } from '../models/album';
import { NgbAlert, NgbRatingConfig } from '@ng-bootstrap/ng-bootstrap';
import { Songs } from '../models/Songs';
import { debounceTime } from 'rxjs/operators';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css'],
  providers: [NgbRatingConfig],
  styles: [
    `
      .star {
        font-size: 1.5rem;
        color: #b0c4de;
      }
      .filled {
        color: #1e90ff;
      }
      .bad {
        color: #deb0b0;
      }
      .filled.bad {
        color: #ff1e1e;
      }
    `,
  ],
})
export class DetailsComponent implements OnInit {
  constructor(
    private route: ActivatedRoute,
    private DataServiceService: DataServiceService,
    config: NgbRatingConfig
  ) {
    config.max = 5;
  }
  private _success = new Subject<string>();

  albums: Album[] = [];
  allAlbums: Array<Album> = [];
  alb: Album;
  staticAlertClosed = false;
  successMessage = '';
  currentRate = 5;
  @ViewChild('staticAlert', { static: false }) staticAlert: NgbAlert;
  @ViewChild('selfClosingAlert', { static: false }) selfClosingAlert: NgbAlert;

  // @Output()
  // onmarcogay = new EventEmitter<number>();
  // <app-details  (onmarcogay)="change($event)" ></app-details>

  ngOnInit(): void {
    
    this._success.subscribe((message) => (this.successMessage = message));
    this._success.pipe(debounceTime(5000)).subscribe(() => {
      if (this.selfClosingAlert) {
        this.selfClosingAlert.close();
      }
    });

    let id = +this.route.snapshot.paramMap.get('id')!;
    this.alb = this.DataServiceService.getAlbumById(id);
  }

  selectChangeHandler(selectedrating: number): void {
    this.DataServiceService.updatetrating(selectedrating, this.alb.id);
  }

  actionMethod(event: any, songID: number) {
    this.DataServiceService.updateAvailability(
      (event.target.disabled = true),
      songID,
      this.alb.id
    );

    console.log(songID);
  }

  actionMethodAlbum(event: any, albumid: number) {
    this.DataServiceService.updatealbumAvailability(
      (event.target.disabled = true),
      albumid
    );
  }

  public changeSuccessMessage() {
    this._success.next(`Thanks for your purchase!`);
  }
}
