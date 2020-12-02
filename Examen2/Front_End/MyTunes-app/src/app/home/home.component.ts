import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {DataServiceService} from  '../core/data-service.service'
import { Album } from '../models/album';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private route: ActivatedRoute,private DataServiceService: DataServiceService) { }

  Myalbums: Album[] = [];
  allAlbums: Array<Album> = [];
  alb: Album;
  ngOnInit(): void {
    // this.Myalbums = this.DataServiceService.getAlbums();
    this.allAlbums = this.DataServiceService.getAlbums();






    this.DataServiceService.getmyalbums()
      .subscribe(
        (albums: Album[]) => {
          this.allAlbums = albums;
          console.log(albums);

        },
        error => console.log(error)
      )


    
  }


  buy_album(buyalbum :  boolean,idalbum : number){

    console.log(buyalbum,idalbum);

    
    this.DataServiceService.buyAlbum(idalbum,buyalbum).subscribe();

  
  }


  actionMethod(event:any, albumid:number) {

  

    this.DataServiceService.updatealbumAvailability( event.target.disabled = true,albumid);





  }

}
