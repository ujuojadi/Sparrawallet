import axios from "axios";

const transfer = async (reference, token) => {
  const reqConfig = {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  };

 const response = await axios.get(
  `${process.env.REACT_APP_API_URL}/transactions/verify?reference=${reference}`,
  reqConfig
);

  return response.data;
};

const transferService = {
  transfer,
};

export default transferService;
