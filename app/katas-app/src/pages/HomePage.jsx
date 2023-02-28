import { Button, Grid, List, ListItem, ListItemButton, ListItemIcon, ListItemText, TextField, Typography } from '@mui/material';
import { Box } from '@mui/system';
import { useEffect, useState } from 'react';
import { postNuevaTirada } from '../helpers/postNuevaTirada';
import { startGame } from '../helpers/startGame';
import Swal from 'sweetalert2';
import 'sweetalert2/src/sweetalert2';

export const HomePage = () => {
    const arrayJugadores = [];
    const [jugadas, setJugadas] = useState([]);
    const [inputJugadores, setInputJugadores] = useState('');
    const [currentPlayer, setCurrentPlayer] = useState(1);
    const [gameIsStarted, setGameIsStarted] = useState(false);

    const handleNuevaTirada = async () => {
        const player = `user ${currentPlayer}`;
        const lastPlayer = jugadas.filter(x => x.username === player).slice(-1).pop();
        const initialSquare = !lastPlayer ? 1 : lastPlayer.resultSquare;
        const { isWon, resultSquare, username, dieRoll } = await postNuevaTirada({ username: player, initialSquare });
        setJugadas([...jugadas.filter(x => x.username !== player), { resultSquare, username, dieRoll, id: lastPlayer.id }]);
        if (isWon) {
            Swal.fire(`Ha ganado el jugador ${player}, estaba en la casilla ${lastPlayer.resultSquare} y saco un ${dieRoll}`, 'Juego finalizado!!', 'success');
            setGameIsStarted(false);
            setCurrentPlayer(0);
            setJugadas([]);
            setInputJugadores('');
        }
        const nextPlayer = currentPlayer + 1;
        setCurrentPlayer(nextPlayer <= jugadas.length ? nextPlayer : 1);
    }

    useEffect(() => {
        if (inputJugadores === 0) setJugadas([]);
        else {
            for (let index = 1; index <= inputJugadores; index++) {
                arrayJugadores.push({ username: `user ${index}`, dieRoll: '', resultSquare: 1, id: index })
            }
            setCurrentPlayer(1);
        }
    }, [inputJugadores]);

    const handleStartGame = async () => {
        await startGame({ username: '' });
        setJugadas(arrayJugadores);
        setGameIsStarted(true);
    }

    const handleResetGame = () => {
        setJugadas([]);
        setGameIsStarted(false);
        setCurrentPlayer(1);
        setInputJugadores('');
    }

    return (
        <>
            <Grid
                container
                direction='column'
                justifyContent='center'
                alignItems='center'
                sx={{ mb: 2 }} >
                <Grid item>
                    <TextField
                        id="outlined-basic"
                        label="Cantidad de jugadores"
                        variant="outlined"
                        type='number'
                        onChange={(e) => setInputJugadores(e.target.value)}
                        value={inputJugadores}
                    />
                </Grid>
            </Grid>
            {
                !gameIsStarted &&
                <Grid
                    container
                    direction='column'
                    justifyContent='center'
                    alignItems='center'
                    sx={{ mb: 2 }}>
                    <Grid item>
                        <Button variant="contained" onClick={handleStartGame} disabled={inputJugadores === '' || inputJugadores <= 1}>Comenzar el juego</Button>
                    </Grid>
                </Grid>
            }
            {
                gameIsStarted && (
                    <>
                        <Grid
                            container
                            direction='column'
                            justifyContent='center'
                            alignItems='center'
                            sx={{ mb: 2 }}>
                            <Grid item>
                                <Button variant="contained" onClick={handleResetGame}>Reiniciar juego</Button>
                            </Grid>
                        </Grid>
                        <Grid
                            container
                            direction='row'
                            justifyContent='space-between'
                            alignItems='center'>
                            <Grid item sx={{ mr: 1 }}>
                                <Typography variant="h5" gutterBottom>
                                    {`Turno: User ${currentPlayer}`}
                                </Typography>
                            </Grid>
                            <Grid item>
                                <Button variant="contained" onClick={handleNuevaTirada}>Tirar</Button>
                            </Grid>
                        </Grid>
                        <Box sx={{ width: '100%', maxWidth: 360, bgcolor: 'background.paper' }}>
                            <List>
                                {
                                    jugadas.map(j => (
                                        <ListItem key={j.id} disablePadding>
                                            <ListItemButton>
                                                <ListItemText primary={`${j.username} - Dado ${j.dieRoll} - Casilla actual: ${j.resultSquare}`} />
                                            </ListItemButton>
                                        </ListItem>
                                    ))
                                }
                            </List>
                        </Box>
                    </>

                )
            }

        </>
    )
}