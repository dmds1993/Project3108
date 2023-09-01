import React, { Component } from 'react';
import { Paper, Typography, Button, CircularProgress, Modal, Box } from '@mui/material';
import {useNavigate, useParams} from 'react-router-dom';
import request from '../api/request';

class TaskDetails extends Component {
  constructor(props) {
    super(props);

    this.state = {
      task: {},
      loading: null,
      taskNotFound: null,
      isModalOpen: false,
      id: props.id
    };
  }

  toggleModal = () => {
    this.setState(prevState => ({
      isModalOpen: !prevState.isModalOpen,
    }));
  };


  async componentDidMount() {
    try {
      console.log(this.state)
      const response = await request.get(`api/task/${this.state.id}`);
      console.log(response.data)
      this.setState({ task: response.data });
    } catch (error) {
      console.error('Error fetching task details:', error);
    }
  }

  render() {
    const { loading } = this.state;
    const { taskNotFound } = this.state;
    const { task } = this.state;
    
    if (loading) {
      return (
        <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
          <CircularProgress />
        </div>
      );
    }

    if (taskNotFound) {
      return (
        <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
          <Typography variant="h6">Task not found</Typography>
        </div>
      );
    }

    return (
      <Paper style={{ padding: '16px' }}>
        <Typography variant="h6" gutterBottom>
          Task Details
        </Typography>
        <Typography variant="subtitle1">Name: {task.name}</Typography>
        <Typography variant="subtitle1">Description: {task.description}</Typography>
        <Typography variant="subtitle1">Deadline: {task.deadline}</Typography>
        <img
          src={`data:image/png;base64,${task.image}`}
          alt="Task"
          style={{ cursor: 'pointer', maxWidth: '100%', marginTop: '8px' }}
          onClick={this.toggleModal}
        />
        <Modal open={this.state.isModalOpen} onClose={this.toggleModal}>
          <div style={{ position: 'absolute', top: '50%', left: '50%', transform: 'translate(-50%, -50%)' }}>
            <img src={`data:image/png;base64,${task.image}`} alt="Task" style={{ maxWidth: '400px' }} />
          </div>
        </Modal>
        <Box display="flex" justifyContent="left">
        <Button onClick={() => this.props.navigate("/column")} variant="contained" color="primary" style={{ marginTop: '16px' }}>
          Back to Column
        </Button>
        </Box>
      </Paper>
    );
  }
}

function WithNavigate(props) {
  let navigate = useNavigate();
  let params = useParams();
  console.log(params.taskid)
  return <TaskDetails {...props} navigate={navigate} id={params.taskid} />
}

export default WithNavigate;
