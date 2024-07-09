# Используем LTS версию Node.js для надежности
FROM node:18 AS build

# Устанавливаем рабочую директорию внутри контейнера
WORKDIR /app

COPY . .

# Устанавливаем зависимости
RUN npm install 
RUN npm install -g nx@19.4.2
# Копируем все файлы проекта в контейнер


# # Сборка микрофронта для продакшена (или замените на `build:development` для отладки)
# RUN npx nx build host --configuration production

# # Используем Nginx для сервировки статических файлов
# FROM nginx:alpine

# # Копируем собранные файлы из предыдущего этапа
# COPY --from=build /app/dist/apps/host /usr/share/nginx/html

# # Копируем файл конфигурации Nginx
# COPY nginx.conf /etc/nginx/nginx.conf

# Указываем порт, который будет открыт для доступа
EXPOSE 80

# Запускаем Nginx
CMD ["npx", "nx", "serve", "host", "--host", "0.0.0.0", "--cwd", "/app"]
