
            using System;
            using System.Data;
            using System.Data.SqlClient;
            
             namespace Hotel_Business
             {
                 public  class clssysdiagrams
                 {
                           
                          
            public enum enMode {AddNew = 0,Update = 1}
            public static enMode Mode = enMode.AddNew;
            
                          public sysdiagramsDTO sysdiagramsDTO
{
    get
    {
        return new sysdiagramsDTO(
	  this.name, 
	  this.principal_id, 
	  this.diagram_id, 
	  this.version, 
	  this.definition, 
       );
    }
}

                          	 public string name  {get; set;}
	 public int principal_id  {get; set;}
	 public int diagram_id  {get; set;}
	 public int version  {get; set;}
	 public byte[] definition  {get; set;}

                          
public clssysdiagrams() { 	
 this.name = "";
	
 this.principal_id = -1;
	
 this.diagram_id = -1;
	
 this.version = -1;
	
 this.definition = byte[];

    Mode = enMode.AddNew;
}
                          
            public clssysdiagrams(sysdiagramsDTO sysdiagrams, enMode mode = enMode.AddNew ){

this.name = sysdiagrams.name ;

this.principal_id = sysdiagrams.principal_id ;

this.diagram_id = sysdiagrams.diagram_id ;

this.version = sysdiagrams.version ;

this.definition = sysdiagrams.definition ;

Mode = mode;
}
                          public static List<sysdiagramsDTO> GetAllsysdiagrams()
{
return clssysdiagramsData.GetAllsysdiagrams();
 
}

                          
             public static clssysdiagrams GetsysdiagramsInfoByID(int diagram_id)
                    {
                   sysdiagramsDTO sysdiagramsDTO = clssysdiagramsData.GetsysdiagramsInfoByID(diagram_id);

                    if (sysdiagramsDTO != null)
                    {
                        return new clssysdiagrams(sysdiagramsDTO, enMode.Update);
                    }
                    else
                    {
                        return null;
                    }
            }
                
                          private  bool _AddNewsysdiagrams()
{

this.diagram_id = (int)clssysdiagramsData.AddNewsysdiagrams(  this.sysdiagramsDTO);
            return (this.diagram_id != -1);
             
}

                          private  bool _Updatesysdiagrams()
{

                return (clssysdiagramsData.Updatesysdiagrams(this.sysdiagramsDTO));
}

                          public  bool Save()
{

if (Mode == enMode.AddNew)
    {
        if (_AddNewsysdiagrams())
        {
            Mode = enMode.Update;
            return true;
        }
        else
            return false;
    }
    else
    {
        return _Updatesysdiagrams();
    }

}

                          public static bool Deletesysdiagrams(int diagram_id)
{
return clssysdiagramsData.Deletesysdiagrams(diagram_id);
 
}


                        
                 }
             } 
                
            
             