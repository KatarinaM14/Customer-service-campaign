import * as React from 'react';
import {
  useNavigate,
  Link as RouterLink
} from 'react-router-dom';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import FormControlLabel from '@mui/material/FormControlLabel';
import Checkbox from '@mui/material/Checkbox';
import Link from '@mui/material/Link';
import Paper from '@mui/material/Paper';
import Box from '@mui/material/Box';
import Grid from '@mui/material/Grid';
import Alert from '@mui/material/Alert';
import Snackbar from '@mui/material/Snackbar';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import { login } from '../api/apiService';
import { useUser } from '../context/UserContext';


const defaultTheme = createTheme();

export default function SignInSide() {
  const { user, setUser } = useUser();
  const [open, setOpen] = React.useState(false);
  const [message, setMessage] = React.useState('');
  const [successOpen, setSuccessOpen] = React.useState(false);
  const [successMessage, setSuccessMessage] = React.useState('');
  let navigate = useNavigate();

  const handleClose = (event, reason) => {
    if (reason === 'clickaway') {
      return;
    }
   
    setOpen(false);
  };

  const handleSubmit = async (event) => {
    event.preventDefault();
    const data = new FormData(event.currentTarget);
    const username = data.get('username');
    const password = data.get('password');
 
    try {
      const data = await login(username, password);
        console.log(data)
      setSuccessMessage('Logged in successfully! Redirecting to homepage');
      setSuccessOpen(true);

      const token = data.token;
      localStorage.setItem('token', token);
      setUser({ token, userData:data.user });

      setTimeout(() => {
        navigate('/rewardCustomer');
      }, 1500);
    } catch (e) {
      setMessage('Failed to login: ' + e.message);
      setOpen(true);
    }
  };

  return (
    <ThemeProvider theme={defaultTheme}>
      <Grid style={{backgroundColor:'#1e3d7b'}}  justifyContent={'center'} alignContent={'center'} container component="main" sx={{ height: '100vh' }}>
        <CssBaseline />
        <Grid alignContent={'center'} item xs={12} sm={8} md={3} component={Paper} elevation={6} square>
          <Box
            sx={{
              my: 8,
              mx: 4,
              display: 'flex',
              flexDirection: 'column',
              alignItems: 'center',
            }}
          >
            <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
              <LockOutlinedIcon />
            </Avatar>
            <Typography component="h1" variant="h5">
              Sign in
            </Typography>
            <Box component="form" noValidate onSubmit={handleSubmit} sx={{ mt: 1 }}>
              <TextField
                margin="normal"
                required
                fullWidth
                id="username"
                label="Username"
                name="username"
                autoComplete="username"
                autoFocus
              />
              <TextField
                margin="normal"
                required
                fullWidth
                name="password"
                label="Password"
                type="password"
                id="password"
                autoComplete="current-password"
              />
              <Button
                type="submit"
                fullWidth
                variant="contained"
                sx={{ mt: 3, mb: 2 }}
              >
                Sign In
              </Button>
              <Grid container>
                <Grid item>
                  <Link
                    component={RouterLink}
                    to="/signup"
                    variant="body2"
                  >
                    {"Don't have an account? Sign Up"}
                  </Link>
                </Grid>
              </Grid>
            </Box>
          </Box>
        </Grid>
        <Snackbar open={open} autoHideDuration={4000} onClose={handleClose} anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }} >
          <Alert onClose={handleClose} severity="error" sx={{ width: '100%' }}>
            {message}
          </Alert>
        </Snackbar>
        <Snackbar 
          open={successOpen} 
          autoHideDuration={4000} 
          onClose={() => setSuccessOpen(false)}
          anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
          >
          <Alert onClose={() => setSuccessOpen(false)} severity="success" sx={{ width: '100%' }}>
            {successMessage}
          </Alert>
          </Snackbar>
      </Grid>
    </ThemeProvider>
  );
}
