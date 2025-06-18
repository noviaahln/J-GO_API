using Npgsql;
using System;
using System.Collections.Generic;

namespace J_GO_API.Models
{
    public class RiwayatContext
    {
        private readonly string _connectionString;

        public RiwayatContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        // CREATE
        public void TambahRiwayat(Riwayat riwayat)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var command = new NpgsqlCommand(
                @"INSERT INTO ""Riwayat"" 
                  (jenis_layanan, lokasi_jemput, lokasi_tujuan, jarak, harga, waktu_pemesanan, status_order)
                  VALUES (@jenis_layanan, @lokasi_jemput, @lokasi_tujuan, @jarak, @harga, @waktu_pemesanan, @status_order)",
                connection);

            command.Parameters.AddWithValue("@jenis_layanan", riwayat.jenis_layanan);
            command.Parameters.AddWithValue("@lokasi_jemput", riwayat.lokasi_jemput);
            command.Parameters.AddWithValue("@lokasi_tujuan", riwayat.lokasi_tujuan);
            command.Parameters.AddWithValue("@jarak", riwayat.jarak);
            command.Parameters.AddWithValue("@harga", riwayat.harga);
            command.Parameters.AddWithValue("@waktu_pemesanan", riwayat.waktu_pemesanan);
            command.Parameters.AddWithValue("@status_order", riwayat.status_order);

            command.ExecuteNonQuery();
        }

        // READ (semua data)
        public List<Riwayat> GetSemuaRiwayat()
        {
            var list = new List<Riwayat>();
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var command = new NpgsqlCommand(@"SELECT * FROM ""Riwayat""", connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Riwayat
                {
                    id_order = reader.GetInt32(0),
                    jenis_layanan = reader.GetString(1),
                    lokasi_jemput = reader.GetString(2),
                    lokasi_tujuan = reader.GetString(3),
                    jarak = reader.GetInt32(4),
                    harga = reader.GetInt32(5),
                    waktu_pemesanan = reader.GetDateTime(6),
                    status_order = reader.GetString(7)
                });
            }

            return list;
        }

        // READ (berdasarkan id_order)
        public Riwayat? GetRiwayatById(int id_order)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var command = new NpgsqlCommand(@"SELECT * FROM ""Riwayat"" WHERE id_order = @id_order", connection);
            command.Parameters.AddWithValue("@id_order", id_order);

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new Riwayat
                {
                    id_order = reader.GetInt32(0),
                    jenis_layanan = reader.GetString(1),
                    lokasi_jemput = reader.GetString(2),
                    lokasi_tujuan = reader.GetString(3),
                    jarak = reader.GetInt32(4),
                    harga = reader.GetInt32(5),
                    waktu_pemesanan = reader.GetDateTime(6),
                    status_order = reader.GetString(7)
                };
            }

            return null;
        }

        // UPDATE
        public void UpdateRiwayat(Riwayat riwayat)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var command = new NpgsqlCommand(
                @"UPDATE ""Riwayat"" SET 
                    jenis_layanan = @jenis_layanan,
                    lokasi_jemput = @lokasi_jemput,
                    lokasi_tujuan = @lokasi_tujuan,
                    jarak = @jarak,
                    harga = @harga,
                    waktu_pemesanan = @waktu_pemesanan,
                    status_order = @status_order
                  WHERE id_order = @id_order",
                connection);

            command.Parameters.AddWithValue("@jenis_layanan", riwayat.jenis_layanan);
            command.Parameters.AddWithValue("@lokasi_jemput", riwayat.lokasi_jemput);
            command.Parameters.AddWithValue("@lokasi_tujuan", riwayat.lokasi_tujuan);
            command.Parameters.AddWithValue("@jarak", riwayat.jarak);
            command.Parameters.AddWithValue("@harga", riwayat.harga);
            command.Parameters.AddWithValue("@waktu_pemesanan", riwayat.waktu_pemesanan);
            command.Parameters.AddWithValue("@status_order", riwayat.status_order);
            command.Parameters.AddWithValue("@id_order", riwayat.id_order);

            command.ExecuteNonQuery();
        }

        // DELETE
        public void HapusRiwayat(int id_order)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var command = new NpgsqlCommand(@"DELETE FROM ""Riwayat"" WHERE id_order = @id_order", connection);
            command.Parameters.AddWithValue("@id_order", id_order);
            command.ExecuteNonQuery();
        }
    }
}
