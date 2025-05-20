import React, { createContext, useState, useContext, ReactNode } from 'react';

// Define the shape of the context
interface SearchContextProps {
  destination: string;
  setDestination: React.Dispatch<React.SetStateAction<string>>;
  minPrice: number;
  setMinPrice: React.Dispatch<React.SetStateAction<number>>;
  maxPrice: number;
  setMaxPrice: React.Dispatch<React.SetStateAction<number>>;
  selectionRange: {
    startDate: Date;
    endDate: Date;
    key: string;
  };
  setSelectionRange: React.Dispatch<
    React.SetStateAction<{
      startDate: Date;
      endDate: Date;
      key: string;
    }>
  >;
  occupancy: {
    adult: number;
    children: number;
    room: number;
  };
  setOccupancy: React.Dispatch<
    React.SetStateAction<{
      adult: number;
      children: number;
      room: number;
    }>
  >;
  resetSearch: () => void;
}

// Create the context with an undefined initial value
const SearchContext = createContext<SearchContextProps | undefined>(undefined);

interface SearchProviderProps {
  children: ReactNode;
}



// Provider component that holds the state and updater functions
export const SearchProvider: React.FC<SearchProviderProps> = ({ children }) => {
  const [destination, setDestination] = useState<string>("");
  const [minPrice, setMinPrice] = useState<number>(0);
  const [maxPrice, setMaxPrice] = useState<number>(550);
  
  const [selectionRange, setSelectionRange] = useState({
    startDate: new Date(),
    endDate: new Date(),
    key: "selection",
  });
  
  const [occupancy, setOccupancy] = useState({
    adult: 1,
    children: 0,
    room: 1,
  });


// This function resets all search-related state to its default values
const resetSearch = () => {
  setDestination("");
  setSelectionRange({
    startDate: new Date(),
    endDate: new Date(),
    key: "selection",
  });
  setOccupancy({
    adult: 1,
    children: 0,
    room: 1,
  });
};

  const value: SearchContextProps = {
    destination,
    setDestination,
    selectionRange,
    setSelectionRange,
    occupancy,
    setOccupancy,
    resetSearch,
    minPrice, setMinPrice,
    maxPrice, setMaxPrice
  };

  return (
    <SearchContext.Provider value={value}>
      {children}
    </SearchContext.Provider>
  );
};

// Custom hook for easier consumption of the context
export const useSearchContext = () => {
  const context = useContext(SearchContext);
  if (context === undefined) {
    throw new Error('useSearchContext must be used within a SearchProvider');
  }
  return context;
};
