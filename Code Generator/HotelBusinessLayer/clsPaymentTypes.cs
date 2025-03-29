
            using System;
            using System.Data;
            using System.Data.SqlClient;
            
             namespace Hotel_Business
             {
                 public  class clsPaymentTypes
                 {
                           
                          
            public enum enMode {AddNew = 0,Update = 1}
            public static enMode Mode = enMode.AddNew;
            
                          public PaymentTypesDTO paymenttypesDTO
{
    get
    {
        return new PaymentTypesDTO(
	  this.PaymentTypeID, 
	  this.PaymentTypeName, 
       );
    }
}

                          	 public int PaymentTypeID  {get; set;}
	 public string PaymentTypeName  {get; set;}

                          
public clsPaymentTypes() { 	
 this.PaymentTypeID = -1;
	
 this.PaymentTypeName = "";

    Mode = enMode.AddNew;
}
                          
            public clsPaymentTypes(PaymentTypesDTO paymenttypes, enMode mode = enMode.AddNew ){

this.PaymentTypeID = paymenttypes.PaymentTypeID ;

this.PaymentTypeName = paymenttypes.PaymentTypeName ;

Mode = mode;
}
                          public static List<PaymentTypesDTO> GetAllPaymentTypes()
{
return clsPaymentTypesData.GetAllPaymentTypes();
 
}

                          
             public static clsPaymentTypes GetPaymentTypesInfoByID(int PaymentTypeID)
                    {
                   PaymentTypesDTO paymenttypesDTO = clsPaymentTypesData.GetPaymentTypesInfoByID(PaymentTypeID);

                    if (paymenttypesDTO != null)
                    {
                        return new clsPaymentTypes(paymenttypesDTO, enMode.Update);
                    }
                    else
                    {
                        return null;
                    }
            }
                
                          private  bool _AddNewPaymentTypes()
{

this.PaymentTypeID = (int)clsPaymentTypesData.AddNewPaymentTypes(  this.paymenttypesDTO);
            return (this.PaymentTypeID != -1);
             
}

                          private  bool _UpdatePaymentTypes()
{

                return (clsPaymentTypesData.UpdatePaymentTypes(this.paymenttypesDTO));
}

                          public  bool Save()
{

if (Mode == enMode.AddNew)
    {
        if (_AddNewPaymentTypes())
        {
            Mode = enMode.Update;
            return true;
        }
        else
            return false;
    }
    else
    {
        return _UpdatePaymentTypes();
    }

}

                          public static bool DeletePaymentTypes(int PaymentTypeID)
{
return clsPaymentTypesData.DeletePaymentTypes(PaymentTypeID);
 
}


                        
                 }
             } 
                
            
             