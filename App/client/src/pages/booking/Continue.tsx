// src/pages/booking/[bookingId]/continue.tsx
import { useParams, useNavigate } from 'react-router-dom';
import { useState } from 'react';
import axios from 'axios';
import toast from 'react-hot-toast';
import { format } from 'date-fns';
import useFetch from '../../hooks/useFetch';
import { number } from 'zod';

type Step = 'guests' | 'payment' | 'review';

const paymentTypes = ['Cash', 'Card', 'Bank Transfer'];
const currencies   = ['USD', 'EUR'];
interface BookingFees{
  bookingId:number;
  totalPrice:number;
}
interface Guest { fullName: string; idNumber:string; }
interface BookingPayload {
  bookingId: string;
  guests: Guest[];
  payment: { type: number; amount: number; currency: string; };
}

export default function BookingContinue() {
  const navigate = useNavigate();
  const { bookingId } = useParams<{ bookingId: string }>();
  const [step, setStep] = useState<Step>('guests');

  const { data  , loadingFees, errorFees } = useFetch({
    url: `/Booking/${bookingId}/booking-fees`
  });


  const bookingFees = data as BookingFees | null;
  // Guests
  const [guestCount, setGuestCount] = useState(0);
  const [guests,    setGuests]     = useState<Guest[]>([]);

  // Payment
  const [paymentType, setPaymentType] = useState('Card');
  const [currency,    setCurrency]    = useState(currencies[0]);
  const [cardNumber,  setCardNumber]  = useState('');
  const [expiry,      setExpiry]      = useState('');
  const [cvc,         setCvc]         = useState('');
  const [transferId,  setTransferId]  = useState('');

  const [loading, setLoading] = useState(false);

  // Helpers
  const canNextGuests = guestCount >= 0 && guests.length === guestCount && guests.every(g => g.fullName.trim());
  const canNextPayment = 
    parseFloat( bookingFees?.totalPrice) > 0 &&
    (paymentType === 'Card' ? cardNumber && expiry && cvc
      : paymentType === 'Bank Transfer' ? transferId.trim()
      : true);

  function renderStepper() {
    const steps: Step[] = ['guests','payment','review'];
    return (
      <div className="flex items-center  justify-center mb-8 space-x-4">
        {steps.map(s => (
          <div key={s} className="flex items-center ">
            <div
              className={`w-8 h-8 flex items-center justify-center rounded-full ${
                step===s ? 'bg-blue-600 text-white' : 'bg-gray-200 text-gray-600'
              }`}
            >{steps.indexOf(s)+1}</div>
            {s !== 'review' && <div className="w-[11rem] h-1 bg-gray-300"></div>}
          </div>
        ))}
      </div>
    );
  }

  const handleSubmit = async () => {
    const payload: BookingPayload = {
      bookingId: bookingId!,
      guests,
      payment: {
        type: paymentType=="Cash"?1:paymentType=="Card"?2:3,
        amount: parseFloat(bookingFees?.totalPrice),
        currency
      }
    };
    
    setLoading(true);
    try {
      const response =await axios.post(`${import.meta.env.VITE_API_URL}/booking/${bookingId}/complete`, payload);
      if(response.data.success){
      toast.success('✅ Reservation completed!');
      navigate(`/my-bookings`);
    }else{
      toast.error("Not Completed Payment");
    }
    } catch (e:any) {
      toast.error(e.message || 'Submission failed');
    } finally {
      setLoading(false);
    }
  };

  return (
    <>
     

      <main className="min-h-screen bg-gray-50 py-12 px-4">
        <div className="max-w-3xl mx-auto bg-white rounded-2xl shadow-xl p-8">
          <h1 className="text-3xl font-bold text-center text-blue-700 mb-2">
            Continue Reservation — #{bookingId}
          </h1>
          <p className="text-center text-gray-500 mb-8">
            {format(new Date(), 'PPP')}
          </p>

          {renderStepper()}

          {/* STEP 1: Guests */}
          {/* STEP 1: Guests */}
{step === 'guests' && (
  <div className="space-y-6">
    <label className="block">
      <span className="text-gray-700">Number of Guests</span>
      <input
        type="number"
        min={0}
        max={5}
        value={guestCount}
        onChange={e => {
          const c = Math.min(5, Math.max(0, +e.target.value));
          setGuestCount(c);
          // rebuild array with default shape { fullName: '', idNumber: '' }
          setGuests(Array.from({ length: c }, (_, i) => guests[i] || { fullName: '', idNumber: '' }));
        }}
        className="mt-1 block w-full bg-gray-100 p-3 text-gray-800 focus:bg-white focus:ring-2 focus:ring-blue-400"
      />
    </label>

    {guests.map((g, i) => (
      <div key={i} className="grid grid-cols-1 md:grid-cols-2 gap-4">
        {/* Full Name */}
        <label className="block">
          <span className="text-gray-700">Guest {i + 1} Full Name</span>
          <input
            type="text"
            value={g.fullName}
            onChange={e => {
              const arr = [...guests];
              arr[i].fullName = e.target.value;
              setGuests(arr);
            }}
            placeholder="John Doe"
            className="mt-1 block w-full bg-gray-100 p-3 text-gray-800 focus:bg-white focus:ring-2 focus:ring-blue-400"
          />
        </label>

        {/* ID Number */}
        <label className="block">
          <span className="text-gray-700">Guest {i + 1} ID Number</span>
          <input
            type="text"
            value={g.idNumber}
            onChange={e => {
              const arr = [...guests];
              arr[i].idNumber = e.target.value;
              setGuests(arr);
            }}
            placeholder="A123456789"
            className="mt-1 block w-full bg-gray-100 p-3 text-gray-800 focus:bg-white focus:ring-2 focus:ring-blue-400"
          />
        </label>
      </div>
    ))}

    <div className="flex justify-end">
      <button
        disabled={
          guestCount === 0
            ? false
            : !(guests.length === guestCount && guests.every(g => g.fullName.trim() && g.idNumber.trim()))
        }
        onClick={() => setStep('payment')}
        className="btn-primary disabled:opacity-50"
      >
        Next: Payment ➔
      </button>
    </div>
  </div>
)}


          {/* STEP 2: Payment */}
          {step==='payment' && (
            <div className="space-y-6">
              <label className="block">
                <span className="text-gray-700">Payment Type</span>
                <select
                  value={paymentType}
                  onChange={e=>setPaymentType(e.target.value)}
                  className="mt-1 block w-full border-gray-300 p-2 shadow-sm focus:ring-blue-500"
                >
                  {paymentTypes.map(t=>(
                    <option key={t}>{t}</option>
                  ))}
                </select>
              </label>

              <label className="block">
                <span className="text-gray-700">Amount ({currency})</span>
                {loadingFees && <h2>Loading</h2>}
                {errorFees && <h2 className='text-red-500'>error</h2>}
                {bookingFees?.totalPrice && 
                <input
                  type="number" 
                  min={0}
                  value={bookingFees?.totalPrice}
                  className="mt-1 block w-full border-gray-300 p-2 shadow-sm focus:ring-blue-500"
                  placeholder="0.00"
                  readOnly
                />}
                
              </label>

              <label className="block">
                <span className="text-gray-700">Currency</span>
                <select
                  value={currency}
                  onChange={e=>setCurrency(e.target.value)}
                  className="mt-1 block w-full border-gray-300 p-2 shadow-sm focus:ring-blue-500"
                >
                  {currencies.map(c=>(
                    <option key={c}>{c}</option>
                  ))}
                </select>
              </label>

              {paymentType==='Card' && (
                <>
                  <label className="block">
                    <span className="text-gray-700">Card Number</span>
                    <input
                      type="text" value={cardNumber}
                      onChange={e=>setCardNumber(e.target.value)}
                      className="mt-1 block w-full border-gray-300 p-2 shadow-sm focus:ring-blue-500"
                      placeholder="•••• •••• •••• ••••"
                    />
                  </label>
                  <div className="grid grid-cols-2 gap-4">
                    <label className="block">
                      <span className="text-gray-700">Expiry (MM/YY)</span>
                      <input
                        type="text" value={expiry}
                        onChange={e=>setExpiry(e.target.value)}
                        className="mt-1 block w-full border-gray-300 p-2 shadow-sm focus:ring-blue-500"
                      />
                    </label>
                    <label className="block">
                      <span className="text-gray-700">CVC</span>
                      <input
                        type="text" value={cvc}
                        onChange={e=>setCvc(e.target.value)}
                        className="mt-1 block w-full border-gray-300 p-2 shadow-sm focus:ring-blue-500"
                      />
                    </label>
                  </div>
                </>
              )}

              {paymentType==='Bank Transfer' && (
                <label className="block">
                  <span className="text-gray-700">Transfer Transaction ID</span>
                  <input
                    type="text" value={transferId}
                    onChange={e=>setTransferId(e.target.value)}
                    className="mt-1 block w-full border-gray-300 p-2 shadow-sm focus:ring-blue-500"
                    placeholder="e.g. TRX123456789"
                  />
                </label>
              )}

              <div className="flex justify-between">
                <button
                  onClick={()=>setStep('guests')}
                  className="btn-secondary"
                >← Back</button>
                <button
                  disabled={!canNextPayment}
                  onClick={()=>setStep('review')}
                  className="btn-primary disabled:opacity-50"
                >
                  Next: Review ➔
                </button>
              </div>
            </div>
          )}

          {/* STEP 3: Review & Submit */}
          {step==='review' && (
            <div className="space-y-6">
              <h2 className="text-2xl font-semibold text-gray-800">Review & Confirm</h2>
              <div className="space-y-4 text-gray-700">
                <p>
                  <strong>Guests:</strong> {guestCount} —
                  {guests.map(g=>g.fullName).join(', ')}
                </p>
                <p><strong>Payment:</strong> {paymentType} {bookingFees?.totalPrice} {currency}</p>
                {paymentType==='Card' && <p>Card ending ••••{cardNumber.slice(-4)}</p>}
                {paymentType==='Bank Transfer' && <p>Transfer ID: {transferId}</p>}
              </div>
              <div className="flex justify-between">
                <button
                  onClick={()=>setStep('payment')}
                  className="btn-secondary"
                >← Back</button>
                <button
                  onClick={handleSubmit}
                  disabled={loading}
                  className="btn-primary disabled:opacity-50"
                >
                  {loading ? 'Processing…' : '✅ Confirm Reservation'}
                </button>
              </div>
            </div>
          )}
        </div>
      </main>
    </>
  );
}
