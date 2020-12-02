import { Component, OnInit } from '@angular/core';
import { Album } from '../models/album';
import {DataServiceService} from  '../core/data-service.service'
import { Songs } from '../models/Songs';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  constructor(private DataServiceService: DataServiceService) { }
  albums: Album[] = [];
  allAlbums: Array<Album> = [];



  ngOnInit(): void {
    this.albums = this.DataServiceService.getAlbums();
    this.allAlbums = this.DataServiceService.getAlbums();
  }



  AlbumTime(album:Album){

    let totaltime=0;

    album.songs.forEach(Songs=>{totaltime+=Songs.duration});

    return Math.round(totaltime * 100)/100;




  }

}
