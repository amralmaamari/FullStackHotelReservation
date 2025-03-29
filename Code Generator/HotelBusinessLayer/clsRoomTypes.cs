
            using System;
            using System.Data;
            using System.Data.SqlClient;
            
             namespace Hotel_Business
             {
                 public  class clsRoomTypes
                 {
                           
                          
            public enum enMode {AddNew = 0,Update = 1}
            public static enMode Mode = enMode.AddNew;
            
                          public RoomTypesDTO roomtypesDTO
{
    get
    {
        return new RoomTypesDTO(
	  this.RoomTypeID, 
	  this.Title, 
	  this.MaxPeople, 
	  this.Description, 
	  this.CreatedAt, 
	  this.UpdateAt, 
       );
    }
}

                          	 public int RoomTypeID  {get; set;}
	 public string Title  {get; set;}
	 public int MaxPeople  {get; set;}
	 public string Description  {get; set;}
	 public DateTime CreatedAt  {get; set;}
	 public DateTime UpdateAt  {get; set;}

                          
public clsRoomTypes() { 	
 this.RoomTypeID = -1;
	
 this.Title = "";
	
 this.MaxPeople = -1;
	
 this.Description = "";
	
 this.CreatedAt = DateTime.Now;
	
 this.UpdateAt = DateTime.Now;

    Mode = enMode.AddNew;
}
                          
            public clsRoomTypes(RoomTypesDTO roomtypes, enMode mode = enMode.AddNew ){

this.RoomTypeID = roomtypes.RoomTypeID ;

this.Title = roomtypes.Title ;

this.MaxPeople = roomtypes.MaxPeople ;

this.Description = roomtypes.Description ;

this.CreatedAt = roomtypes.CreatedAt ;

this.UpdateAt = roomtypes.UpdateAt ;

Mode = mode;
}
                          public static List<RoomTypesDTO> GetAllRoomTypes()
{
return clsRoomTypesData.GetAllRoomTypes();
 
}

                          
             public static clsRoomTypes GetRoomTypesInfoByID(int RoomTypeID)
                    {
                   RoomTypesDTO roomtypesDTO = clsRoomTypesData.GetRoomTypesInfoByID(RoomTypeID);

                    if (roomtypesDTO != null)
                    {
                        return new clsRoomTypes(roomtypesDTO, enMode.Update);
                    }
                    else
                    {
                        return null;
                    }
            }
                
                          private  bool _AddNewRoomTypes()
{

this.RoomTypeID = (int)clsRoomTypesData.AddNewRoomTypes(  this.roomtypesDTO);
            return (this.RoomTypeID != -1);
             
}

                          private  bool _UpdateRoomTypes()
{

                return (clsRoomTypesData.UpdateRoomTypes(this.roomtypesDTO));
}

                          public  bool Save()
{

if (Mode == enMode.AddNew)
    {
        if (_AddNewRoomTypes())
        {
            Mode = enMode.Update;
            return true;
        }
        else
            return false;
    }
    else
    {
        return _UpdateRoomTypes();
    }

}

                          public static bool DeleteRoomTypes(int RoomTypeID)
{
return clsRoomTypesData.DeleteRoomTypes(RoomTypeID);
 
}


                        
                 }
             } 
                
            
             