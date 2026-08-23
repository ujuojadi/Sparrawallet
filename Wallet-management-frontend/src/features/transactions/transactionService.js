import axios from "axios";
const API_URL = `${process.env.REACT_APP_API_URL}/transactions/`;


// get user account infos
const getTransactions = async (id, token) => {
  const config = {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  };
  const response = await axios.get(API_URL + id, config);
  return response.data;
};

const transactionService = {
  getTransactions,
};

export default transactionService;
