
            using System;
            using System.Data;
            using System.Data.SqlClient;
            
             namespace Hotel_Business
             {
                 public  class clsRooms
                 {
                           
                          
            public enum enMode {AddNew = 0,Update = 1}
            public static enMode Mode = enMode.AddNew;
            
                          public RoomsDTO roomsDTO
{
    get
    {
        return new RoomsDTO(
	  this.RoomID, 
	  this.RoomTypeID, 
	  this.HotelID, 
	  this.RoomNumber, 
	  this.Price, 
	  this.CreatedAt, 
	  this.UpdateAt, 
       );
    }
}

                          	 public int RoomID  {get; set;}
	 public int RoomTypeID  {get; set;}
	 public int HotelID  {get; set;}
	 public string RoomNumber  {get; set;}
	 public decimal Price  {get; set;}
	 public DateTime CreatedAt  {get; set;}
	 public DateTime UpdateAt  {get; set;}

                          
public clsRooms() { 	
 this.RoomID = -1;
	
 this.RoomTypeID = -1;
	
 this.HotelID = -1;
	
 this.RoomNumber = "";
	
 this.Price = 0m;
	
 this.CreatedAt = DateTime.Now;
	
 this.UpdateAt = DateTime.Now;

    Mode = enMode.AddNew;
}
                          
            public clsRooms(RoomsDTO rooms, enMode mode = enMode.AddNew ){

this.RoomID = rooms.RoomID ;

this.RoomTypeID = rooms.RoomTypeID ;

this.HotelID = rooms.HotelID ;

this.RoomNumber = rooms.RoomNumber ;

this.Price = rooms.Price ;

this.CreatedAt = rooms.CreatedAt ;

this.UpdateAt = rooms.UpdateAt ;

Mode = mode;
}
                          public static List<RoomsDTO> GetAllRooms()
{
return clsRoomsData.GetAllRooms();
 
}

                          
             public static clsRooms GetRoomsInfoByID(int RoomID)
                    {
                   RoomsDTO roomsDTO = clsRoomsData.GetRoomsInfoByID(RoomID);

                    if (roomsDTO != null)
                    {
                        return new clsRooms(roomsDTO, enMode.Update);
                    }
                    else
                    {
                        return null;
                    }
            }
                
                          private  bool _AddNewRooms()
{

this.RoomID = (int)clsRoomsData.AddNewRooms(  this.roomsDTO);
            return (this.RoomID != -1);
             
}

                          private  bool _UpdateRooms()
{

                return (clsRoomsData.UpdateRooms(this.roomsDTO));
}

                          public  bool Save()
{

if (Mode == enMode.AddNew)
    {
        if (_AddNewRooms())
        {
            Mode = enMode.Update;
            return true;
        }
        else
            return false;
    }
    else
    {
        return _UpdateRooms();
    }

}

                          public static bool DeleteRooms(int RoomID)
{
return clsRoomsData.DeleteRooms(RoomID);
 
}


                        
                 }
             } 
                
            
             