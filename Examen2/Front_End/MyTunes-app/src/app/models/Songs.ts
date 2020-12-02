export class Songs{

    constructor(
        public idsong: number,
        public name: string,
        public artist: string,
        public price: number,
        public duration: number,
        public rating: number,
        public buyclick: boolean,
    )
    {}

}