export interface IPhoto {
    PhotoURL: string;
  }
  export interface IRoom {
    RoomID: number;
    Title: string;
    Price: number;
    MaxPeople: number;
    RoomNumber: string;
  }
  
  export interface IHotel {
    hotelID: number;
    typeName: string;
    name: string;
    city: string;
    address: string;
    distance: string;
    photos: string;
    rooms: IRoom[];
    title: string;
    description: string;
    cheapestPrice: number;
    featured: boolean;
  }
  