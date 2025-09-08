import {
  Box,
  Button,
  ButtonGroup,
  List,
  ListItemText,
  Paper,
  Typography,
} from "@mui/material";
import { useStore } from "../../lib/hooks/useStore";
import { observer } from "mobx-react-lite";

const counter = observer(function Counter() {
  const { counterStore } = useStore();

  return (
    <Box display="flex" justifyContent="space-between">
      <Box sx={{ width: "60%" }}>
        <Typography variant="h4" gutterBottom>
          {counterStore.title}
        </Typography>
        <Typography variant="h6" gutterBottom>
          The count is {counterStore.count}
        </Typography>

        <ButtonGroup sx={{ mt: 3 }}>
          <Button
            variant="contained"
            color="error"
            onClick={() => counterStore.decrement()}
          >
            Decrement
          </Button>
          <Button
            variant="contained"
            color="primary"
            onClick={() => counterStore.increment()}
          >
            increment
          </Button>
          <Button
            variant="contained"
            color="secondary"
            onClick={() => counterStore.increment(5)}
          >
            increment by 5
          </Button>
        </ButtonGroup>
      </Box>
      <Paper sx={{ width: "40%", p: 4 }}>
        <Typography variant="h5">
          Counter events ({counterStore.eventCount})
        </Typography>
          
        <List>
          {counterStore.events.map((ev, index) => {
            return <ListItemText key={index}>{ev}</ListItemText>;
          })}
        </List>
      </Paper>
    </Box>
  );
});

export default counter;
