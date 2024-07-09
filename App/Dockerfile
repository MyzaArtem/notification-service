FROM node:18 as node
WORKDIR /usr/src/app

COPY package.json package-lock.json /usr/src/
RUN npm install --prefer-offline --no-audit
COPY . .
RUN npx kendo-ui-license activate
RUN npx nx build host

FROM nginx:stable
COPY nginx.conf /etc/nginx/nginx.conf
COPY --from=node /usr/src/app/dist/apps/host /usr/share/nginx/html
