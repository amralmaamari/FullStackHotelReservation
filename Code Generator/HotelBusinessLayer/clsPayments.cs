
            using System;
            using System.Data;
            using System.Data.SqlClient;
            
             namespace Hotel_Business
             {
                 public  class clsPayments
                 {
                           
                          
            public enum enMode {AddNew = 0,Update = 1}
            public static enMode Mode = enMode.AddNew;
            
                          public PaymentsDTO paymentsDTO
{
    get
    {
        return new PaymentsDTO(
	  this.PaymentID, 
	  this.BookingID, 
	  this.PaymentTypeID, 
	  this.PaymentStatusID, 
	  this.Amount, 
	  this.CurrencyCode, 
	  this.TimeAndDate, 
       );
    }
}

                          	 public int PaymentID  {get; set;}
	 public int BookingID  {get; set;}
	 public int PaymentTypeID  {get; set;}
	 public int PaymentStatusID  {get; set;}
	 public decimal Amount  {get; set;}
	 public string CurrencyCode  {get; set;}
	 public DateTime TimeAndDate  {get; set;}

                          
public clsPayments() { 	
 this.PaymentID = -1;
	
 this.BookingID = -1;
	
 this.PaymentTypeID = -1;
	
 this.PaymentStatusID = -1;
	
 this.Amount = 0m;
	
 this.CurrencyCode = "";
	
 this.TimeAndDate = DateTime.Now;

    Mode = enMode.AddNew;
}
                          
            public clsPayments(PaymentsDTO payments, enMode mode = enMode.AddNew ){

this.PaymentID = payments.PaymentID ;

this.BookingID = payments.BookingID ;

this.PaymentTypeID = payments.PaymentTypeID ;

this.PaymentStatusID = payments.PaymentStatusID ;

this.Amount = payments.Amount ;

this.CurrencyCode = payments.CurrencyCode ;

this.TimeAndDate = payments.TimeAndDate ;

Mode = mode;
}
                          public static List<PaymentsDTO> GetAllPayments()
{
return clsPaymentsData.GetAllPayments();
 
}

                          
             public static clsPayments GetPaymentsInfoByID(int PaymentID)
                    {
                   PaymentsDTO paymentsDTO = clsPaymentsData.GetPaymentsInfoByID(PaymentID);

                    if (paymentsDTO != null)
                    {
                        return new clsPayments(paymentsDTO, enMode.Update);
                    }
                    else
                    {
                        return null;
                    }
            }
                
                          private  bool _AddNewPayments()
{

this.PaymentID = (int)clsPaymentsData.AddNewPayments(  this.paymentsDTO);
            return (this.PaymentID != -1);
             
}

                          private  bool _UpdatePayments()
{

                return (clsPaymentsData.UpdatePayments(this.paymentsDTO));
}

                          public  bool Save()
{

if (Mode == enMode.AddNew)
    {
        if (_AddNewPayments())
        {
            Mode = enMode.Update;
            return true;
        }
        else
            return false;
    }
    else
    {
        return _UpdatePayments();
    }

}

                          public static bool DeletePayments(int PaymentID)
{
return clsPaymentsData.DeletePayments(PaymentID);
 
}


                        
                 }
             } 
                
            
             