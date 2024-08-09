const apiUrl = process.env.REACT_APP_API_URL;

export const register = async (firstName, lastName, username, password) => {
   

    const user = JSON.stringify({ firstName,lastName, username, password })

    const response = await fetch(`https://localhost:7208/Auth/Register`, {
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
    const response = await fetch(`https://localhost:7208/Auth/LogIn`, {
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

export const rewardCustomer = async (rewardCustomer, userToken) => {
	console.log(rewardCustomer)
	console.log(userToken);
	const response = await fetch(
		`https://localhost:7208/Reward/RewardCustomer`, {
			method: 'POST',
			headers: {
				'Content-Type': 'application/json',
				Authorization: `Bearer ${userToken}`,
			},
			body: JSON.stringify(rewardCustomer),
		}
	)

	if (!response.ok) {
		throw new Error(`HTTP error! Status: ${response.status}`);
	}
	const data = await response;
	return data;
}