cd Backend/Archangel.Tests.WebEditor
dotnet publish -c Debug -o publish
cd ../../Frontend/news-editor
npm install
npm run-script build

cd ../../
docker-compose build
docker-compose up
