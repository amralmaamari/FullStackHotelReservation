
            using System;
            using System.Data;
            using System.Data.SqlClient;
            
             namespace Hotel_Business
             {
                 public  class clsPaymentStatus
                 {
                           
                          
            public enum enMode {AddNew = 0,Update = 1}
            public static enMode Mode = enMode.AddNew;
            
                          public PaymentStatusDTO paymentstatusDTO
{
    get
    {
        return new PaymentStatusDTO(
	  this.PaymentStatusID, 
	  this.StatusName, 
       );
    }
}

                          	 public int PaymentStatusID  {get; set;}
	 public string StatusName  {get; set;}

                          
public clsPaymentStatus() { 	
 this.PaymentStatusID = -1;
	
 this.StatusName = "";

    Mode = enMode.AddNew;
}
                          
            public clsPaymentStatus(PaymentStatusDTO paymentstatus, enMode mode = enMode.AddNew ){

this.PaymentStatusID = paymentstatus.PaymentStatusID ;

this.StatusName = paymentstatus.StatusName ;

Mode = mode;
}
                          public static List<PaymentStatusDTO> GetAllPaymentStatus()
{
return clsPaymentStatusData.GetAllPaymentStatus();
 
}

                          
             public static clsPaymentStatus GetPaymentStatusInfoByID(int PaymentStatusID)
                    {
                   PaymentStatusDTO paymentstatusDTO = clsPaymentStatusData.GetPaymentStatusInfoByID(PaymentStatusID);

                    if (paymentstatusDTO != null)
                    {
                        return new clsPaymentStatus(paymentstatusDTO, enMode.Update);
                    }
                    else
                    {
                        return null;
                    }
            }
                
                          private  bool _AddNewPaymentStatus()
{

this.PaymentStatusID = (int)clsPaymentStatusData.AddNewPaymentStatus(  this.paymentstatusDTO);
            return (this.PaymentStatusID != -1);
             
}

                          private  bool _UpdatePaymentStatus()
{

                return (clsPaymentStatusData.UpdatePaymentStatus(this.paymentstatusDTO));
}

                          public  bool Save()
{

if (Mode == enMode.AddNew)
    {
        if (_AddNewPaymentStatus())
        {
            Mode = enMode.Update;
            return true;
        }
        else
            return false;
    }
    else
    {
        return _UpdatePaymentStatus();
    }

}

                          public static bool DeletePaymentStatus(int PaymentStatusID)
{
return clsPaymentStatusData.DeletePaymentStatus(PaymentStatusID);
 
}


                        
                 }
             } 
                
            
             