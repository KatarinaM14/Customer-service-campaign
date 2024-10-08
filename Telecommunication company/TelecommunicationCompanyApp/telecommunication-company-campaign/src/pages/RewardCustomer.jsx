import * as React from 'react';
import { useEffect, useState } from 'react';
import { CssVarsProvider, useTheme } from '@mui/joy/styles';
import CssBaseline from '@mui/joy/CssBaseline';
import Box from '@mui/joy/Box';
import Stack from '@mui/joy/Stack';
import IconButton from '@mui/material/IconButton';
import Avatar from '@mui/material/Avatar';
import Alert from '@mui/material/Alert';
import Snackbar from '@mui/material/Snackbar';
import Button from '@mui/joy/Button';
import { useNavigate, useParams } from 'react-router-dom';
import { CircularProgress, Textarea, Typography } from '@mui/joy';
import { useUser } from '../context/UserContext';
import Input from '@mui/joy/Input';
import {rewardCustomer} from '../api/apiService'
import { Grid } from '@mui/material';
import { logout } from '../api/apiService'


const RewardField = ({title, ...rest}) => (
	<Box flexGrow={1} minWidth={100}>
		{title}
		<Input {...rest} />
	</Box>
)

export default function RewardCustomer() {

	const navigate = useNavigate();
	const { user, setUser } = useUser();
	
	const reward = {
		customerId: 0,
		userId: user.userData.id,
		description: '',
		discountAmount: 0
	};
	
	const [rewardedCustomer, setRewardedCustomer] = useState(reward);
	
	const handleFieldChange = (event) => {
    	setRewardedCustomer({
      ...rewardedCustomer,
      [event.target.name]: event.target.value,
    });
  };

  const submitRewardCustomer = () => {
	try {
		const data = rewardCustomer(rewardedCustomer,user?.token).then((res) =>{
			setRewardedCustomer(reward)
		}); 
	}catch (e) {
      alert('Failed to reward customer: ' + e.message);
    }
  };

  const onLogOut = async () => {
	  	await logout(user?.token);
	  	setUser(null);
		navigate('/login');
	}

	return (
		<CssVarsProvider>
			<CssBaseline />
			<Box
				component="main"
				sx={{
					height: '100vh', 
					display: 'grid',
					gridTemplateColumns: { xs: 'auto', md: '100%' },
					gridTemplateRows: 'auto 1fr auto',
				}}
			>
				<Stack
					spacing={2}
					sx={{ px: { xs: 2, md: 4 }, pt: 2, minHeight: 0 }}
					height= '100vh'
					marginTop={'50px'}
				>
					<Stack width={'90%'} alignItems={'flex-end'} >
						<Button onClick={()=>onLogOut()}>
								Log out
							</Button>
					</Stack>
					<Stack direction="column" spacing={2} paddingTop={3} alignItems={'center'}>
						<Stack direction="column" spacing={1} width={'30%'} justifyContent={'center'}>
							<RewardField id="description" name="description" title="Description" value={rewardedCustomer?.description}  onChange={handleFieldChange}/>
							<RewardField id="discountAmount" name="discountAmount" title="Discount amount" value={rewardedCustomer?.discountAmount}  onChange={handleFieldChange}/>
							<RewardField id="customerId" name="customerId" title="Customer Id" value={rewardedCustomer?.customerId}  onChange={handleFieldChange}/>					
						</Stack>
						<Button onClick={()=>submitRewardCustomer()}>
							Reward customer
						</Button>
					</Stack>					
				</Stack>
			</Box>	
		</CssVarsProvider>
	);
}
