import {Songs} from  "./Songs";

export class Album{

    constructor(
        public idalbum: number,
        public album_name: string,
        public artist_name: string,
        public price: number,
        public style: string,
        public  rating: number,
        public  duration: number,
        public date_departure: string,
        public description: string,
        public img: string,
        public buyalbum: boolean,
        public msongs: Songs[],
    )
    {}
}