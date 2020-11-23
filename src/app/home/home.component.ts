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

  albums: Album[] = [];
  allAlbums: Array<Album> = [];
  alb: Album;
  ngOnInit(): void {
    this.albums = this.DataServiceService.getAlbums();
    this.allAlbums = this.DataServiceService.getAlbums();






    
  }


  actionMethod(event:any, albumid:number) {

  

    this.DataServiceService.updatealbumAvailability( event.target.disabled = true,albumid);





  }

}
