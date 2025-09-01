import {
  Card,
  CardContent,
  CardActions,
  Chip,
  Button,
  Box,
} from "@mui/material";
import { Typography } from "@mui/material";

type Props = {
  activity: Activity;
  selectActivity: (id: string) => void;
  deleteActivity: (id: string) => void;
};
function ActivityCard({ activity, selectActivity, deleteActivity }: Props) {
  return (
    <Card sx={{ borderRadius: 3 }}>
      <CardContent>
        <Typography variant="h5">{activity.title}</Typography>
        <Typography sx={{ color: "text.secondary", mb: 1 }}>
          {activity.date}
        </Typography>
        <Typography variant="body2">{activity.description}</Typography>
        <Typography variant="subtitle1">
          {activity.city} / {activity.venue}
        </Typography>
      </CardContent>
      <CardActions
        sx={{ display: "flex", justifyContent: "space-between", pb: 2 }}
      >
        <Chip label={activity.category} variant="outlined" />
        <Box display="flex" gap={3}>
          <Button
            variant="contained"
            size="medium"
            onClick={() => selectActivity(activity.id)}
          >
            View
          </Button>
          <Button
            variant="contained"
            size="medium"
            color="error"
            onClick={() => deleteActivity(activity.id)}
          >
            Delete
          </Button>
        </Box>
      </CardActions>
    </Card>
  );
}

export default ActivityCard;
