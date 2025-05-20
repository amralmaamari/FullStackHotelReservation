
            using System;
            using System.Data;
            using System.Data.SqlClient;
            
             namespace Hotel_Business
             {
                 public  class clsHotelPhotos
                 {
                           
                          
            public enum enMode {AddNew = 0,Update = 1}
            public static enMode Mode = enMode.AddNew;
            
                          public HotelPhotosDTO hotelphotosDTO
{
    get
    {
        return new HotelPhotosDTO(
	  this.HotelPhotoID, 
	  this.HotelID, 
	  this.PhotoURL, 
       );
    }
}

                          	 public int HotelPhotoID  {get; set;}
	 public int HotelID  {get; set;}
	 public string PhotoURL  {get; set;}

                          
public clsHotelPhotos() { 	
 this.HotelPhotoID = -1;
	
 this.HotelID = -1;
	
 this.PhotoURL = "";

    Mode = enMode.AddNew;
}
                          
            public clsHotelPhotos(HotelPhotosDTO hotelphotos, enMode mode = enMode.AddNew ){

this.HotelPhotoID = hotelphotos.HotelPhotoID ;

this.HotelID = hotelphotos.HotelID ;

this.PhotoURL = hotelphotos.PhotoURL ;

Mode = mode;
}
                          public static List<HotelPhotosDTO> GetAllHotelPhotos()
{
return clsHotelPhotosData.GetAllHotelPhotos();
 
}

                          
             public static clsHotelPhotos GetHotelPhotosInfoByID(int HotelPhotoID)
                    {
                   HotelPhotosDTO hotelphotosDTO = clsHotelPhotosData.GetHotelPhotosInfoByID(HotelPhotoID);

                    if (hotelphotosDTO != null)
                    {
                        return new clsHotelPhotos(hotelphotosDTO, enMode.Update);
                    }
                    else
                    {
                        return null;
                    }
            }
                
                          private  bool _AddNewHotelPhotos()
{

this.HotelPhotoID = (int)clsHotelPhotosData.AddNewHotelPhotos(  this.hotelphotosDTO);
            return (this.HotelPhotoID != -1);
             
}

                          private  bool _UpdateHotelPhotos()
{

                return (clsHotelPhotosData.UpdateHotelPhotos(this.hotelphotosDTO));
}

                          public  bool Save()
{

if (Mode == enMode.AddNew)
    {
        if (_AddNewHotelPhotos())
        {
            Mode = enMode.Update;
            return true;
        }
        else
            return false;
    }
    else
    {
        return _UpdateHotelPhotos();
    }

}

                          public static bool DeleteHotelPhotos(int HotelPhotoID)
{
return clsHotelPhotosData.DeleteHotelPhotos(HotelPhotoID);
 
}


                        
                 }
             } 
                
            
             