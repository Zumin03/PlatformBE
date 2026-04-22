import {
  Navigate,
  RouterProvider,
  createBrowserRouter,
} from "react-router-dom";
import { AppLayout } from "./components/layout/AppLayout";
import { InstrumentDetailPage } from "./pages/InstrumentDetailPage";
import { InstrumentsPage } from "./pages/InstrumentsPage";
import { MeasurementsPage } from "./pages/MeasurementsPage";

const router = createBrowserRouter([
  {
    path: "/",
    element: <AppLayout />,
    children: [
      {
        index: true,
        element: <Navigate replace to="/measurements" />, // base route
      },
      {
        path: "measurements",
        element: <MeasurementsPage />,
      },
      {
        path: "instruments",
        element: <InstrumentsPage />,
      },
      {
        path: "instruments/:deviceId",
        element: <InstrumentDetailPage />,
      },
    ],
  },
]);

function App() {
  return <RouterProvider router={router} />;
}

export default App;
