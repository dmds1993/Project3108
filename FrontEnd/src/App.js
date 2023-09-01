import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import TaskColumn from './Components/TaskColumn';
import TaskDetails from './Components/TaskDetails';
import Home from './Components/Home';
import TaskBoard from './Components/TaskBoard';

const App = () => {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/column" element={<TaskBoard />} />
        <Route path="/task/:taskid/" element={<TaskDetails />} />
      </Routes>
    </Router>
  );
};

export default App;
