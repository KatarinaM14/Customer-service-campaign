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


const RewardField = ({title, ...rest}) => (
	<Box flexGrow={1} minWidth={100}>
		{title}
		<Input {...rest} />
	</Box>
)

export default function RewardCustomer() {

	const navigate = useNavigate();
	const { user } = useUser();

	console.log(user)
	const reward = {
		customerId: 0,
		userId: user.userData.id,
		description: '',
		discountAmount: 0
	};

	const [rewardedCustomer, setRewardedCustomer] = useState(reward);

	const [successOpen, setSuccessOpen] = React.useState(false);
	const [successMessage, setSuccessMessage] = React.useState('');
	const [open, setOpen] = React.useState(false);
	const [message, setMessage] = React.useState('');

	

	const handleFieldChange = (event) => {
    	setRewardedCustomer({
      ...rewardedCustomer,
      [event.target.name]: event.target.value,
    });
  };

  const submitRewardCustomer = () => {
	try {
		console.log(rewardedCustomer)
		console.log(user?.token)
		const data = rewardCustomer(rewardedCustomer,user?.token).then((res) =>{
			console.log(res)
			setRewardedCustomer(reward)
			setSuccessOpen(true);
			setSuccessMessage(res);
		}); 
	}catch (e) {
      setMessage('Failed to reward customer: ' + e.message);
      setOpen(true);
    }
  };

	return (
		<CssVarsProvider>
			<CssBaseline />
			<Box
				component="main"
				sx={{
					height: 'calc(100vh - 55px)', 
					display: 'grid',
					gridTemplateColumns: { xs: 'auto', md: '100%' },
					gridTemplateRows: 'auto 1fr auto',
				}}
			>
				<Stack
					spacing={2}
					sx={{ px: { xs: 2, md: 4 }, pt: 2, minHeight: 0 }}
					height="100%"
				>
					<Stack direction="column" spacing={2} paddingTop={3}>
						<Stack direction="column" spacing={1}>
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
