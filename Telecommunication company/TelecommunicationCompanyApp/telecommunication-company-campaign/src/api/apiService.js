import config from '../config';

const API_URL = config.apiBaseUrl;

export const register = async (firstName, lastName, username, password) => {
   

    const user = JSON.stringify({ firstName,lastName, username, password })

    const response = await fetch(`${API_URL}/Auth/Register`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Origin': 'http://localhost:5173',
        },
        body: user
    });

    if (!response.ok) {
        const errorData = await response.text();
        console.error('Error response from server:', errorData);
        throw new Error(errorData);
    }

    const data = await response.json();
    return data;
}

export const login = async (username, password) => {
    console.log(API_URL)
    const response = await fetch(`${API_URL}/Auth/LogIn`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Origin': 'http://localhost:5173',
      },
      body: JSON.stringify({ username, password }),
    });
 
    if (!response.ok) {
        const errorData = await response.text();
        console.error('Error response from server:', errorData);
        throw new Error(errorData);
    }

    const data = await response.json();
    return data;
}

export const logout = async (userToken) => {
    const response = await fetch(`${API_URL}/Auth/LogOut`, {
        method: 'POST',
        headers: {
            Authorization: `Bearer ${userToken}`,
        },
    });

    if (!response.ok) {
        const errorData = await response.text();
        console.error('Error response from server:', errorData);
        throw new Error(errorData);
    }
}

export const rewardCustomer = async (rewardCustomer, userToken) => {
	console.log(rewardCustomer)
	console.log(userToken);
	const response = await fetch(
		`${API_URL}/Reward/RewardCustomer`, {
			method: 'POST',
			headers: {
				'Content-Type': 'application/json',
				Authorization: `Bearer ${userToken}`,
			},
			body: JSON.stringify(rewardCustomer),
		}
	)

	if (!response.ok) {
		throw new Error(`HTTP error! Status: ${response}`);
	}
	const data = await response;
	return data;
}