import axios from "axios";
import { useEffect, useState } from "react";


interface LoginCredentials {
    email: string;
    password: string;
  }
const useLogin=(credentials: LoginCredentials)=>{
  const apiUrl = import.meta.env.VITE_API_URL;

  const [response, setresponse] = useState<string | null>(null);
  const [error, setError] = useState<string>("");
  const [loading, setLoading] = useState<boolean>(false);

  useEffect(()=>{
    const fetchData = async () => {
        setLoading(true);
        
        try {
          // Ensure apiUrl is defined before using it
          if (!apiUrl) {
            throw new Error("API URL is not defined");
          }
          const res = await axios.post(`${apiUrl}/Auth/login`,credentials);
          
          if(res.data.success){
            setresponse(credentials.email);
            setLoading(false);
            setError("");
          }
        } catch (err: any) {
          if (err && err.message) {
            setError(err.message);
          } else {
            setError("An unexpected error occurred");
          }
        }
        setLoading(false);
      };
      fetchData();
  },[credentials])

  return {response,error,loading};
}

export default useLogin;