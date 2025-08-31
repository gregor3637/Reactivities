import { useEffect, useState } from "react";
import { List, ListItem, ListItemText, Typography } from "@mui/material";
import axios from "axios";

function App() {
  const title = "Welcome to Reactivities";
  const [activities, setActivities] = useState<Activity[]>([]);

  useEffect(() => {
    axios
      .get<Activity[]>("http://localhost:5000/api/activities")
      .then((response) => setActivities(response.data));
  }, []);

  return (
    <>
      <Typography variant="h4">{title}</Typography>
      <List>
        {activities.map((activities) => (
          <ListItem key={activities.id}>
            <ListItemText primary={activities.title} />
          </ListItem>
        ))}
      </List>
    </>
  );
}

export default App;
