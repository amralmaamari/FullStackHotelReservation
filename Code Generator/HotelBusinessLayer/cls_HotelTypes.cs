
            using System;
            using System.Data;
            using System.Data.SqlClient;
            
             namespace Hotel_Business
             {
                 public  class clsHotelTypes
                 {
                           
                          
            public enum enMode {AddNew = 0,Update = 1}
            public static enMode Mode = enMode.AddNew;
            
                          public HotelTypesDTO hoteltypesDTO
{
    get
    {
        return new HotelTypesDTO(
	  this.HotelTypeID, 
	  this.TypeName, 
       );
    }
}

                          	 public int HotelTypeID  {get; set;}
	 public string TypeName  {get; set;}

                          
public clsHotelTypes() { 	
 this.HotelTypeID = -1;
	
 this.TypeName = "";

    Mode = enMode.AddNew;
}
                          
            public clsHotelTypes(HotelTypesDTO hoteltypes, enMode mode = enMode.AddNew ){

this.HotelTypeID = hoteltypes.HotelTypeID ;

this.TypeName = hoteltypes.TypeName ;

Mode = mode;
}
                          public static List<HotelTypesDTO> GetAllHotelTypes()
{
return clsHotelTypesData.GetAllHotelTypes();
 
}

                          
             public static clsHotelTypes GetHotelTypesInfoByID(int HotelTypeID)
                    {
                   HotelTypesDTO hoteltypesDTO = clsHotelTypesData.GetHotelTypesInfoByID(HotelTypeID);

                    if (hoteltypesDTO != null)
                    {
                        return new clsHotelTypes(hoteltypesDTO, enMode.Update);
                    }
                    else
                    {
                        return null;
                    }
            }
                
                          private  bool _AddNewHotelTypes()
{

this.HotelTypeID = (int)clsHotelTypesData.AddNewHotelTypes(  this.hoteltypesDTO);
            return (this.HotelTypeID != -1);
             
}

                          private  bool _UpdateHotelTypes()
{

                return (clsHotelTypesData.UpdateHotelTypes(this.hoteltypesDTO));
}

                          public  bool Save()
{

if (Mode == enMode.AddNew)
    {
        if (_AddNewHotelTypes())
        {
            Mode = enMode.Update;
            return true;
        }
        else
            return false;
    }
    else
    {
        return _UpdateHotelTypes();
    }

}

                          public static bool DeleteHotelTypes(int HotelTypeID)
{
return clsHotelTypesData.DeleteHotelTypes(HotelTypeID);
 
}


                        
                 }
             } 
                
            
             