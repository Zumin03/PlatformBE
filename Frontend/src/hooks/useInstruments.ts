import { useQuery } from "@tanstack/react-query";
import { getInstrumentById, getInstruments } from "../api/instruments";

export function useInstruments() {
  return useQuery({
    queryKey: ["instruments"],
    queryFn: getInstruments,
  });
}

export function useInstrument(deviceId: string) {
  return useQuery({
    queryKey: ["instruments", deviceId],
    queryFn: () => getInstrumentById(deviceId),
    enabled: Boolean(deviceId),
  });
}
