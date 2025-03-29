
            using System;
            using System.Data;
            using System.Data.SqlClient;
            
             namespace Hotel_Business
             {
                 public  class clsCountries
                 {
                           
                          
            public enum enMode {AddNew = 0,Update = 1}
            public static enMode Mode = enMode.AddNew;
            
                          public CountriesDTO countriesDTO
{
    get
    {
        return new CountriesDTO(
	  this.CountryID, 
	  this.Name, 
	  this.Code, 
       );
    }
}

                          	 public int CountryID  {get; set;}
	 public string Name  {get; set;}
	 public string Code  {get; set;}

                          
public clsCountries() { 	
 this.CountryID = -1;
	
 this.Name = "";
	
 this.Code = "";

    Mode = enMode.AddNew;
}
                          
            public clsCountries(CountriesDTO countries, enMode mode = enMode.AddNew ){

this.CountryID = countries.CountryID ;

this.Name = countries.Name ;

this.Code = countries.Code ;

Mode = mode;
}
                          public static List<CountriesDTO> GetAllCountries()
{
return clsCountriesData.GetAllCountries();
 
}

                          
             public static clsCountries GetCountriesInfoByID(int CountryID)
                    {
                   CountriesDTO countriesDTO = clsCountriesData.GetCountriesInfoByID(CountryID);

                    if (countriesDTO != null)
                    {
                        return new clsCountries(countriesDTO, enMode.Update);
                    }
                    else
                    {
                        return null;
                    }
            }
                
                          private  bool _AddNewCountries()
{

this.CountryID = (int)clsCountriesData.AddNewCountries(  this.countriesDTO);
            return (this.CountryID != -1);
             
}

                          private  bool _UpdateCountries()
{

                return (clsCountriesData.UpdateCountries(this.countriesDTO));
}

                          public  bool Save()
{

if (Mode == enMode.AddNew)
    {
        if (_AddNewCountries())
        {
            Mode = enMode.Update;
            return true;
        }
        else
            return false;
    }
    else
    {
        return _UpdateCountries();
    }

}

                          public static bool DeleteCountries(int CountryID)
{
return clsCountriesData.DeleteCountries(CountryID);
 
}


                        
                 }
             } 
                
            
             