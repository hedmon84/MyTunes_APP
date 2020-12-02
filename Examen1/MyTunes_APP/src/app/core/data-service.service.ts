import { Injectable } from '@angular/core';
import {allAlbums} from './../mocks/album'
import {Album} from './../models/album'

@Injectable({
  providedIn: 'root'
})
export class DataServiceService {

  private readonly  Albums: Album[] = allAlbums;

  constructor() { }
  getAlbums(): Album[] {
    return this.Albums;
  }

  getAlbumById(id: number) {

   
    return this.Albums[id];

  }

  
  updatetrating(rating: number , albumid:number) {

    this.Albums[albumid].rating = rating;
    console.log(rating);



  }

  updateAvailability(available: boolean, albumsongid:number,albumid: number){
    this.Albums[albumid].songs[albumsongid].buyclick = available;
  }

  updatealbumAvailability(available: boolean, albumid:number){
    this.Albums[albumid].buyalbum = available;

    for(let i in this.Albums[albumid].songs){

      this.Albums[albumid].songs[i].buyclick = true;

      console.log(i);

    }


  }

}
