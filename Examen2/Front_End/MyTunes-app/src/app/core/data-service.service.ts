import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {allAlbums} from './../mocks/album';
import {Album} from './../models/album';
import  {catchError} from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class DataServiceService {

  private readonly  Albums: Album[] = allAlbums;


  baseUrl: string =  "https://localhost:5001"

  constructor(private httpClient : HttpClient) { }

  getmyalbums(): Observable<Album[]>{

    return this.httpClient.get<Album[]>(`${this.baseUrl}/home`)
    .pipe(
      catchError(this.handleError)

    )

  }



  getprofile(): Observable<Album[]> {

    return this.httpClient.get<Album[]>(`${this.baseUrl}/home/profile`)
    .pipe(
      catchError(this.handleError)

    )
  }

  gotodetails( albumid : number ): Observable<Album>{

    console.log(albumid);
    

    return this.httpClient.get<Album>(`${this.baseUrl}/home/details/${albumid}`)
    .pipe(
      catchError(this.handleError)

    )

  }

  private handleError(error: any){
    console.error('server error:', error);
    if(error.error instanceof Error){
      const errorMessage = error.error.message;
      return Observable.throw(errorMessage);
    }



    return Observable.throw(error || 'Server error');
  }

  getAlbums(): Album[] {
    return this.Albums;
  }

  getAlbumById(id: number) {

   
    return this.Albums[id];

  }


  buyAlbum(idalb : number , buyclick : Boolean){


    console.log(idalb);
    

    return this.httpClient.post<Album>(`${this.baseUrl}/home/details/${idalb}/buy/album`,buyclick?1:0)
    .pipe(
      catchError(this.handleError)
    )



  }



  buysong(albumid : number ,  songid : number,buyclick : Boolean): Observable<Album>{


    console.log(albumid);
    

    return this.httpClient.post<Album>(`${this.baseUrl}/home/details/${albumid}/buy/song/${songid}`,buyclick?1:0)
    .pipe(
      catchError(this.handleError)
    )



  }


  updateRate(albumid : number , rating : number){



 

    console.log(albumid,rating);
    

    return this.httpClient.post<Album>(`${this.baseUrl}/home/details/${albumid}/rank/update`,rating)
    .pipe(
      catchError(this.handleError)

    )

  }

  
  updatetrating(rating: number , albumid:number) {

    this.Albums[albumid].rating = rating;
    console.log(rating);



  }

  updateAvailability(available: boolean, albumsongid:number,albumid: number){
    this.Albums[albumid].msongs[albumsongid].buyclick = available;
  }

  updatealbumAvailability(available: boolean, albumid:number){
    this.Albums[albumid].buyalbum = available;

    for(let i in this.Albums[albumid].msongs){

      this.Albums[albumid].msongs[i].buyclick = true;

      console.log(i);

    }


  }

}
