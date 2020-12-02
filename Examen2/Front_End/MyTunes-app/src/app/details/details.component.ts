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

    console.log(this.route.snapshot);
    
    let id = parseInt(this.route.snapshot.params['id'],10);
    // this.alb = this.DataServiceService.getAlbumById(id);

    
    this.DataServiceService.gotodetails(id)
    .subscribe(
      (albums: Album) => {
        this.alb = albums;
        console.log(albums);

      },
      error => console.log(error)
    )

  }

  selectChangeHandler(selectedrating: number): void {
    // this.DataServiceService.updatetrating(selectedrating, this.alb.idalbum);
    this.DataServiceService.updateRate(this.alb.idalbum,selectedrating).subscribe();

   
  

  }

  // actionMethod(event: any, songID: number) {
  //   // this.DataServiceService.updateAvailability(
  //   //   (event.target.disabled = true),
  //   //   songID,
  //   //   this.alb.idalbum
  //   // );



  //   this.DataServiceService.buysong(this.alb.idalbum,songID,true).subscribe();

  //   console.log(songID);
  // }


  buy_album( buyalbum :  boolean){

    console.log(buyalbum,this.alb.idalbum);

    
    this.DataServiceService.buyAlbum(this.alb.idalbum,buyalbum).subscribe();

    
    

    
  }

  buy_song(songid : number){

    this.DataServiceService.buysong(this.alb.idalbum,songid,true).subscribe();
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
