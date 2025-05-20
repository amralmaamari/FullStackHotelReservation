
            using System;
            using System.Data;
            using System.Data.SqlClient;
            
             namespace Hotel_Business
             {
                 public  class clsPhoneNumbers
                 {
                           
                          
            public enum enMode {AddNew = 0,Update = 1}
            public static enMode Mode = enMode.AddNew;
            
                          public PhoneNumbersDTO phonenumbersDTO
{
    get
    {
        return new PhoneNumbersDTO(
	  this.PhoneNumbersID, 
	  this.UserID, 
	  this.PhoneNumber, 
	  this.IsPrimary, 
	  this.CreatedAt, 
	  this.UpdatedAt, 
       );
    }
}

                          	 public int PhoneNumbersID  {get; set;}
	 public int UserID  {get; set;}
	 public string PhoneNumber  {get; set;}
	 public bool IsPrimary  {get; set;}
	 public DateTime CreatedAt  {get; set;}
	 public DateTime UpdatedAt  {get; set;}

                          
public clsPhoneNumbers() { 	
 this.PhoneNumbersID = -1;
	
 this.UserID = -1;
	
 this.PhoneNumber = "";
	
 this.IsPrimary = false;
	
 this.CreatedAt = DateTime.Now;
	
 this.UpdatedAt = DateTime.Now;

    Mode = enMode.AddNew;
}
                          
            public clsPhoneNumbers(PhoneNumbersDTO phonenumbers, enMode mode = enMode.AddNew ){

this.PhoneNumbersID = phonenumbers.PhoneNumbersID ;

this.UserID = phonenumbers.UserID ;

this.PhoneNumber = phonenumbers.PhoneNumber ;

this.IsPrimary = phonenumbers.IsPrimary ;

this.CreatedAt = phonenumbers.CreatedAt ;

this.UpdatedAt = phonenumbers.UpdatedAt ;

Mode = mode;
}
                          public static List<PhoneNumbersDTO> GetAllPhoneNumbers()
{
return clsPhoneNumbersData.GetAllPhoneNumbers();
 
}

                          
             public static clsPhoneNumbers GetPhoneNumbersInfoByID(int PhoneNumbersID)
                    {
                   PhoneNumbersDTO phonenumbersDTO = clsPhoneNumbersData.GetPhoneNumbersInfoByID(PhoneNumbersID);

                    if (phonenumbersDTO != null)
                    {
                        return new clsPhoneNumbers(phonenumbersDTO, enMode.Update);
                    }
                    else
                    {
                        return null;
                    }
            }
                
                          private  bool _AddNewPhoneNumbers()
{

this.PhoneNumbersID = (int)clsPhoneNumbersData.AddNewPhoneNumbers(  this.phonenumbersDTO);
            return (this.PhoneNumbersID != -1);
             
}

                          private  bool _UpdatePhoneNumbers()
{

                return (clsPhoneNumbersData.UpdatePhoneNumbers(this.phonenumbersDTO));
}

                          public  bool Save()
{

if (Mode == enMode.AddNew)
    {
        if (_AddNewPhoneNumbers())
        {
            Mode = enMode.Update;
            return true;
        }
        else
            return false;
    }
    else
    {
        return _UpdatePhoneNumbers();
    }

}

                          public static bool DeletePhoneNumbers(int PhoneNumbersID)
{
return clsPhoneNumbersData.DeletePhoneNumbers(PhoneNumbersID);
 
}


                        
                 }
             } 
                
            
             