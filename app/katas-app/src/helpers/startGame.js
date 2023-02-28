export const startGame = async ({ username }) => {
  const requestOptions = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({
      username,
      initialSquare: 1,
      spacesToMoved: 0,
      gameIsStarted: true,
    }),
  };
  const response = await fetch(
    "https://localhost:44363/Token/squares",
    requestOptions
  );
  const data = await response.json();
  return {
    ...data,
  };
};
