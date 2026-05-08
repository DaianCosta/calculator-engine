npm install mongodb

// export.js
import { MongoClient, ObjectId } from 'mongodb';
import fs from 'fs';

const uri = 'SUA_URI_MONGO';
const dbName = 'SEU_DB';
const collectionName = 'SUA_COLLECTION';
const documentId = 'ID_DO_DOCUMENTO';

async function exportHtml() {
  const client = new MongoClient(uri);

  try {
    await client.connect();

    const db = client.db(dbName);
    const collection = db.collection(collectionName);

    const doc = await collection.findOne({
      _id: new ObjectId(documentId),
    });

    if (!doc) {
      console.log('Documento não encontrado');
      return;
    }

    let html = doc.conteudo;

    // 🔥 trata escape (caso tenha \" \\n etc)
    try {
      html = JSON.parse(`"${html}"`);
    } catch (e) {
      // fallback leve
      html = html
        .replace(/\\"/g, '"')
        .replace(/\\n/g, '\n')
        .replace(/\\t/g, '\t');
    }

    fs.writeFileSync('template.html', html);
    console.log('✅ HTML exportado para template.html');
  } finally {
    await client.close();
  }
}

exportHtml();




// import.js
import { MongoClient, ObjectId } from 'mongodb';
import fs from 'fs';

const uri = 'SUA_URI_MONGO';
const dbName = 'SEU_DB';
const collectionName = 'SUA_COLLECTION';
const documentId = 'ID_DO_DOCUMENTO';

async function importHtml() {
  const client = new MongoClient(uri);

  try {
    await client.connect();

    const db = client.db(dbName);
    const collection = db.collection(collectionName);

    const html = fs.readFileSync('template.html', 'utf-8');

    await collection.updateOne(
      { _id: new ObjectId(documentId) },
      { $set: { conteudo: html } }
    );

    console.log('✅ HTML atualizado no Mongo');
  } finally {
    await client.close();
  }
}

importHtml();