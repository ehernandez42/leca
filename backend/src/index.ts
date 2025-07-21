import express, { Express, Request, Response , Application } from 'express';
// import dotenv from 'dotenv'; not sure why this is not working..?

//For env File 
// dotenv.config();

const app: Application = express();
const port = process.env.PORT || 8000;

app.get('/', (req: Request, res: Response) => {
  res.send('Welcome to Express & TypeScript Server');
});

app.listen(port, () => {
  console.log(`Server is Fired at https://localhost:${port}`);
});