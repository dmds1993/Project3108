import React from 'react';
import { Link } from 'react-router-dom';
import { Typography, Button, Container } from '@mui/material';

const Home = () => {
  return (
    <Container style={{ display: 'flex', flexDirection: 'column', alignItems: 'center', justifyContent: 'center', minHeight: '100vh' }}>
      <Typography variant="h4" gutterBottom>
        Welcome to Task Management App!
      </Typography>
      <Typography variant="body1" paragraph>
        This is the home page where you can start managing your tasks.
      </Typography>
      <Button component={Link} to="/column" variant="contained" color="primary">
        Create Column
      </Button>
    </Container>
  );
};

export default Home;
