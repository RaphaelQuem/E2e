cd Client
REM docker build -t "e2e-front:react" .
REM docker build -t "e2e-back:netcore" .

docker run -it --rm -v %cd%:/app -v /app/node_modules -p 3001:3000 -e CHOKIDAR_USEPOLLING=true e2e-front:react

pause >nul
