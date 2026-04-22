const API_BASE_URL =
  import.meta.env.VITE_API_BASE_URL ?? "http://localhost:5209";

export async function fetchJson<T>(path: string): Promise<T> {
  const response = await fetch(`${API_BASE_URL}${path}`);

  if (!response.ok) {
    throw new Error(`Request failed with status ${response.status}`);
  }

  return response.json() as Promise<T>;
}

export async function postJson<TResponse>(
  path: string,
  body?: unknown,
): Promise<TResponse> {
  const response = await fetch(`${API_BASE_URL}${path}`, {
    method: "POST",
    headers:
      body === undefined
        ? undefined
        : {
            "Content-Type": "application/json",
          },
    body: body === undefined ? undefined : JSON.stringify(body),
  });

  if (!response.ok) {
    throw new Error(`Request failed with status ${response.status}`);
  }

  if (response.status === 204) {
    return undefined as TResponse;
  }

  const contentType = response.headers.get("content-type");

  if (!contentType?.includes("application/json")) {
    return undefined as TResponse;
  }

  return response.json() as Promise<TResponse>;
}
