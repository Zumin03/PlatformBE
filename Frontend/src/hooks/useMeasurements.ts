import { useQuery } from "@tanstack/react-query";
import { getMeasurements } from "../api/measurements";

export function useMeasurements() {
  return useQuery({
    queryKey: ["measurements"],
    queryFn: getMeasurements,
  });
}
