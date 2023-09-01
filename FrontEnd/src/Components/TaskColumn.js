import React, { useState, useEffect } from 'react';
import { 
  Paper, Typography, List, ListItem, ListItemText, TextField, Button, Input, InputLabel, FormControl, Box } from '@mui/material';
import { Link } from 'react-router-dom';
import request from '../api/request';

const TaskColumn = ({ columnName, pbiId, tasks, handleGetList }) => {
  const [newTask, setNewTask] = useState('');
  const [newTaskDescription, setNewTaskDescription] = useState('');
  const [newTaskDeadline, setNewTaskDeadline] = useState('');
  const [imageFile, setNewTaskImage] = useState(null);
  const handleRemoveTask = async (item) => {
    await removeTaskFromServer(item.id);
  };

  const removeTaskFromServer = async (taskId) => {
    try {
      await request.delete(`api/task/${taskId}`);
      await handleGetList();
    } catch (error) {
      console.error('Error deleting task:', error);
      return tasks; // Return the current tasks to prevent state update
    }
  };

  const createTaskOnServer = async (newTaskData) => {
    try {
      const reader = new FileReader();
      console.log(newTaskData)
      reader.readAsDataURL(newTaskData.image);
      reader.onload = async () => {
        const imageBase64 = reader.result.split(',')[1];
    
        // Create the new task data with the BASE64 image
        const payload = {
          name: newTaskData.name,
          description: newTaskData.description,
          deadline: newTaskData.deadline,
          image: imageBase64,
          pbiId: pbiId
        };
        console.log(payload)
        await request.post('api/task', [payload]);
      };
    } catch (error) {
    }
  };
  

  const handleAddTask = async () => {
    const task = {
      name: newTask,
      description: newTaskDescription,
      deadline: newTaskDeadline,
      image: imageFile,
    };
    
    setNewTask('');
    setNewTaskDescription('');
    setNewTaskDeadline('');
    setNewTaskImage(null);
    await createTaskOnServer(task);

    await handleGetList();

  };

  const handleImageChange = event => {
    const file = event.target.files[0];
    setNewTaskImage(file);
  };

  return (
    <Paper style={{ padding: '16px' }}>
      <Box display="flex" justifyContent="center">
        <List>
          {tasks != null && tasks.length > 0 ? (
            <List>
              {tasks.map((task, index) => (
                <ListItem key={index}>
                  <ListItemText primary={task.name} secondary={task.description} />
                  <Typography variant="body2">
                    <Link to={`/task/${index}`}>
                      View Details
                    </Link>
                    <Box display="flex" justifyContent="left">
                      <Button onClick={() => handleRemoveTask(task)} color="secondary">
                        Remove
                      </Button>
                    </Box>
                  </Typography>
                </ListItem>
              ))}
            </List>
          ) : (
            <Typography align="center" variant="body2">
              No tasks in this column.
            </Typography>
          )}
        </List>
      </Box>
      <Box mt={2}>
        <TextField
          label="New Task"
          value={newTask}
          onChange={e => setNewTask(e.target.value)}
          fullWidth
          margin="dense"
        />
        <TextField
          label="Description"
          value={newTaskDescription}
          onChange={e => setNewTaskDescription(e.target.value)}
          fullWidth
          margin="dense"
        />
        <TextField
          label="Deadline"
          type="date"
          value={newTaskDeadline}
          onChange={e => setNewTaskDeadline(e.target.value)}
          fullWidth
          margin="dense"
          InputLabelProps={{
            shrink: true,
          }}
        />
        <FormControl fullWidth margin="dense">
          <InputLabel htmlFor="task-image">Image</InputLabel>
          <Input
            id="task-image"
            type="file"
            accept="image/*"
            onChange={handleImageChange}
          />
        </FormControl>
        <Box mt={2} display="flex" justifyContent="center">
          <Button onClick={handleAddTask} variant="contained" color="primary">
            Add Task
          </Button>
        </Box>
      </Box>
    </Paper>
  );
};

export default TaskColumn;