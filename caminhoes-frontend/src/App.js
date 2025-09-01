import React, { useEffect, useState } from 'react';
import axios from 'axios';

const api = axios.create({
  baseURL: process.env.REACT_APP_API_URL + '/api/caminhoes',
});

const modelos = ['FH', 'FM', 'VM'];
const plantas = ['Brasil', 'Suecia', 'EstadosUnidos', 'Franca'];

function App() {
  const [caminhoes, setCaminhoes] = useState([]);
  const [form, setForm] = useState({
    modelo: 'FH',
    anoFabricacao: 2023,
    codigoChassi: '',
    cor: '',
    planta: 'Brasil',
  });
  const [editChassi, setEditChassi] = useState(null);
  const [msg, setMsg] = useState('');

  // Listar caminhões
  const fetchCaminhoes = async () => {
    try {
      const res = await api.get('/');
      setCaminhoes(res.data);
    } catch {
      setMsg('Erro ao buscar caminhões');
    }
  };

  useEffect(() => {
    fetchCaminhoes();
  }, []);

  // Manipulação de formulário
  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  // Criar ou atualizar caminhão
  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      if (editChassi) {
        await api.put(`/${editChassi}`, form);
        setMsg('Caminhão atualizado!');
      } else {
        await api.post('/', form);
        setMsg('Caminhão criado!');
      }
      setForm({
        modelo: 'FH',
        anoFabricacao: 2023,
        codigoChassi: '',
        cor: '',
        planta: 'Brasil',
      });
      setEditChassi(null);
      fetchCaminhoes();
    } catch (err) {
      setMsg('Erro: ' + (err.response?.data?.title || err.message));
    }
  };

  // Editar caminhão
  const handleEdit = (caminhao) => {
    setForm({ ...caminhao });
    setEditChassi(caminhao.codigoChassi);
    setMsg('');
  };

  // Excluir caminhão
  const handleDelete = async (codigoChassi) => {
    if (window.confirm('Deseja excluir este caminhão?')) {
      await api.delete(`/${codigoChassi}`);
      setMsg('Caminhão excluído!');
      fetchCaminhoes();
    }
  };

  // Cancelar edição
  const handleCancel = () => {
    setForm({
      modelo: 'FH',
      anoFabricacao: 2023,
      codigoChassi: '',
      cor: '',
      planta: 'Brasil',
    });
    setEditChassi(null);
    setMsg('');
  };

  return (
    <div style={{ maxWidth: 600, margin: 'auto' }}>
      <h1>CRUD Caminhões</h1>
      {msg && <div style={{ color: 'green', marginBottom: 10 }}>{msg}</div>}
      <form onSubmit={handleSubmit}>
        <div>
          <label>Modelo: </label>
          <select name="modelo" value={form.modelo} onChange={handleChange}>
            {modelos.map((m) => (
              <option key={m} value={m}>
                {m}
              </option>
            ))}
          </select>
        </div>
        <div>
          <label>Ano Fabricação: </label>
          <input
            type="number"
            name="anoFabricacao"
            value={form.anoFabricacao}
            onChange={handleChange}
            min={1900}
            max={2100}
            required
          />
        </div>
        <div>
          <label>Código Chassi: </label>
          <input
            type="text"
            name="codigoChassi"
            value={form.codigoChassi}
            onChange={handleChange}
            required
            disabled={!!editChassi}
          />
        </div>
        <div>
          <label>Cor: </label>
          <input
            type="text"
            name="cor"
            value={form.cor}
            onChange={handleChange}
            required
          />
        </div>
        <div>
          <label>Planta: </label>
          <select name="planta" value={form.planta} onChange={handleChange}>
            {plantas.map((p) => (
              <option key={p} value={p}>
                {p}
              </option>
            ))}
          </select>
        </div>
        <button type="submit">{editChassi ? 'Atualizar' : 'Criar'}</button>
        {editChassi && (
          <button type="button" onClick={handleCancel}>
            Cancelar
          </button>
        )}
      </form>

      <h2>Lista de Caminhões</h2>
      <table border="1" cellPadding="5" style={{ width: '100%' }}>
        <thead>
          <tr>
            <th>Modelo</th>
            <th>Ano</th>
            <th>Código Chassi</th>
            <th>Cor</th>
            <th>Planta</th>
            <th>Ações</th>
          </tr>
        </thead>
        <tbody>
          {caminhoes.map((c) => (
            <tr key={c.codigoChassi}>
              <td>{c.modelo}</td>
              <td>{c.anoFabricacao}</td>
              <td>{c.codigoChassi}</td>
              <td>{c.cor}</td>
              <td>{c.planta}</td>
              <td>
                <button onClick={() => handleEdit(c)}>Editar</button>
                <button onClick={() => handleDelete(c.codigoChassi)}>
                  Excluir
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default App;
