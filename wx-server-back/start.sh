#bin/bash
cd /data/zhouli/code/wx-bot-server/wx-server-back/
git pull origin feature/desktop
docker build -f HZY.WebHost/Dockerfile -t wx-bot-server  .
docker rm -f wx-bot-back
docker run -d --name wx-bot-back -p 9901:9901 \
-v /data/zhouli/wx-bot-back/appsettings.Production.json:/app/appsettings.Production.json \
-v /data/zhouli/wx-bot-back/applogs:/app/applogs \
wx-bot-server


