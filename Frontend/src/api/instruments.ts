import { fetchJson, postJson } from "./http";
import type { Instrument } from "../types/instrument";

export async function getInstruments(): Promise<Instrument[]> {
  return fetchJson<Instrument[]>("/api/Instrument");
}

export async function getInstrumentById(deviceId: string): Promise<Instrument> {
  return fetchJson<Instrument>(`/instrument/${deviceId}`);
}

export async function startMeasurement(deviceId: string): Promise<void> {
  await postJson<void>(`/api/Measurement/${deviceId}/measure`);
}

export async function runSelfTest(deviceId: string): Promise<Instrument> {
  return postJson<Instrument>(`/api/Instrument/${deviceId}/self-test`);
}
