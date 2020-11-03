FROM node:13.12.0-alpine


WORKDIR /app
ENV PATH /app/node_modules/.bin:$PATH

COPY package.json ./
COPY package-lock.json ./
RUN npm install
RUN npm install react-scripts@3.4.1 -g
RUN npm install jquery --save
RUN npm install --save-dev @types/jquery
RUN npm install bootstrap --save
RUN npm install react-datepicker --save


#Adicionar app
COPY . ./

CMD ["npm", "start"]