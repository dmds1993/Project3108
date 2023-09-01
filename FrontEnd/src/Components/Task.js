import React from 'react';
import { Card, CardContent, Typography } from '@mui/material';

const Task = ({ task }) => {
  return (
    <Card>
      <CardContent>
        <Typography variant="h6">{task.name}</Typography>
        <Typography>{task.description}</Typography>
        <Typography>{task.deadline}</Typography>
      </CardContent>
    </Card>
  );
};

export default Task;
