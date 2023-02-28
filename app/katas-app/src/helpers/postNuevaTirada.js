import { getRandom } from "./getRandom";

export const postNuevaTirada = async ({ username, initialSquare }) => {
  const dieRoll = getRandom(1, 7);
  const requestOptions = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({
      username,
      initialSquare,
      spacesToMoved: dieRoll,
      gameIsStarted: false,
    }),
  };
  const response = await fetch(
    "https://localhost:44363/Token/squares",
    requestOptions
  );
  const data = await response.json();
  return {
    ...data,
    dieRoll,
  };
};
