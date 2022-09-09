#STAGE 1
#FROM arm64v8/node:latest AS build
FROM node:latest AS build
WORKDIR /usr/src/app
COPY package.json package-lock.json ./
RUN npm install
COPY . .
RUN npm run build

#STAGE 2
#FROM arm64v8/nginx:latest
FROM nginx:latest
COPY nginx.conf /etc/nginx/nginx.conf
COPY --from=build /usr/src/app/dist/mra.angular /usr/share/nginx/html
