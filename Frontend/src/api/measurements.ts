import { fetchJson } from "./http";
import type { Measurement } from "../types/measurement";

export async function getMeasurements(): Promise<Measurement[]> {
  return fetchJson<Measurement[]>("/api/Measurement");
}
