import React, { useState, useEffect } from 'react';
import { Grid, Button, TextField, List, Typography, ListItem, ListItemText, Box } from '@mui/material';
import { Link } from 'react-router-dom';
import TaskColumn from './TaskColumn';
import request from '../api/request';

const TaskBoard = () => {
  const [columns, setColumns] = useState([]);
  const [newColumnName, setNewColumnName] = useState('');

  const handleCreateTask = (columnIndex, newTask) => {
    const updatedColumns = [...columns];
    updatedColumns[columnIndex].tasks.push(newTask);
    setColumns(updatedColumns);
  };

  useEffect(() => {
    async function fetchTasks() {
      try {
        await handleGetList();
      } catch (error) {
        console.error('Error fetching tasks:', error);
      }
    }

    fetchTasks();
  }, []);


  const handleGetList = async () => {
    const { data } = await request.get('api/pbi')

    setColumns(prevColumns => [...data]);
  }

  const handleCreateColumn = async () => {
    if (newColumnName) {
      const newColumn = {
        name: newColumnName,
        taskModels: [],
      };

      await request.post('api/pbi', newColumn)
      await handleGetList();
    }
  };

  return (
    <div>
      <Box justifyContent="center" alignItems="center">
        <Grid container spacing={20} justifyContent="left" style={{ padding: '20px' }}>
          <Grid item xs={4}>
            <TextField
              label="New Column Name"
              value={newColumnName}
              onChange={e => setNewColumnName(e.target.value)}
              fullWidth
            />
            <Box mt={2} display="flex" justifyContent="center">
              <Button onClick={handleCreateColumn} variant="contained" color="primary">
                Add Column
              </Button>
            </Box>
          </Grid>
        </Grid>
      </Box>
      <Grid container spacing={2}>
        {columns.map((column, columnIndex) => (
          <Grid key={columnIndex} item xs={12} sm={6} md={4}>
            <Box display="flex" flexDirection="column" alignItems="center">
              <Typography variant="h6">{column.name}</Typography>
              <TaskColumn
                columnName={column.name}
                pbiId={column.id}
                tasks={column.taskModels}
                handleGetList={() => handleGetList()}
              />
            </Box>
          </Grid>
        ))}
      </Grid>
    </div>
  );
};

export default TaskBoard;
