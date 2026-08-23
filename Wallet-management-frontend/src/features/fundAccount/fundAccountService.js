import axios from "axios";
const API_URL = `${process.env.REACT_APP_API_URL}/wallets/`;


// create new account info
const fund = async (cardData, token) => {
  const config = {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  };
  const response = await axios.post(API_URL + "deposit", cardData, config);
  console.log(response.data);
  return response.data;
};

const fundAccountService = {
  fund,
};

export default fundAccountService;
